using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;
using System.Text;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;
using FmbLib.TypeHandlers.Xna;

#if XNA
using Microsoft.Xna.Framework;
#endif

#if FEZENGINE
using FezEngine.Structure;
#endif

namespace FmbLib {
    public static class FmbUtil {

        private static Regex GenericSplitRegex = new Regex(@"(\[.*?\])");

        private static char[] XNBMagic = { 'X', 'N', 'B' };

        private static Dictionary<string, TypeHandler> TypeHandlerReaderMap = new Dictionary<string, TypeHandler>();
        private static Dictionary<Type, TypeHandler> TypeHandlerTypeMap = new Dictionary<Type, TypeHandler>();
        private static string[] ManifestResourceNames;

		static FmbUtil(){

			{
				#if XNA
				____dotnetassembliesneedtobereferenced____.Add(typeof(Vector3));
				#endif
				#if FEZENGINE
				____dotnetassembliesneedtobereferenced____.Add(typeof(TrileSet));
				#endif
			}

			{
				GeneratedTypeHandlerAssemblies.Add("MonoGame.Framework");
				GeneratedTypeHandlerAssemblies.Add("FezEngine");
				GeneratedTypeHandlerAssemblies.Add("ContentSerialization");
			}

			{
				GeneratedTypeHandlerSpecialTypes.Add("Matrix");
				GeneratedTypeHandlerSpecialTypes.Add("Quaternion");
				GeneratedTypeHandlerSpecialTypes.Add("Vector2");
				GeneratedTypeHandlerSpecialTypes.Add("Vector3");
				GeneratedTypeHandlerSpecialTypes.Add("Vector4");
				GeneratedTypeHandlerSpecialTypes.Add("Color");
				GeneratedTypeHandlerSpecialTypes.Add("BoundingSphere");
			}

		}

        /// <summary>
        /// List of types that need to be accessed so that the assembly containing them gets referenced
        /// </summary>
        private static List<Type> ____dotnetassembliesneedtobereferenced____ = new List<Type>();

        /// <summary>
        /// List of assemblies required for the generated typehandlers.
        /// </summary>
		public static List<string> GeneratedTypeHandlerAssemblies = new List<string>();
		
        /// <summary>
        /// List of types that are not found in BinaryReader, but in XNA's ContentReader
        /// </summary>
        public static List<string> GeneratedTypeHandlerSpecialTypes = new List<string>();

        public static object ReadObject(string input) {
            using (FileStream fis = new FileStream(input, FileMode.Open)) {
                using (BinaryReader reader = new BinaryReader(fis)) {
                    char[] magic = reader.ReadChars(3);
                    return ReadObject(reader, magic[0] == XNBMagic[0] && magic[1] == XNBMagic[1] && magic[2] == XNBMagic[2]);
                }
            }
        }

        public static object ReadObject(BinaryReader reader, bool xnb) {
            TypeHandler handler;

            string[] readerNames = null;
            TypeHandler[] handlers = null;
            int handlerIndex = -1;
            object[] sharedResources = null;
            string typeName = null;

            if (xnb && reader.BaseStream.Position == 3) {
                object[] xnbData = readXNB(reader);
                readerNames = (string[]) xnbData[0];
                handlers = (TypeHandler[]) xnbData[1];
                sharedResources = (object[]) xnbData[2];
                handler = handlers[handlerIndex = (int) xnbData[3]];
            } else if (xnb) {
                throw new InvalidOperationException("Can't read a non-asset object without type from a XNB stream!");
            } else {
                handler = GetTypeHandler(typeName = reader.ReadString());
            }

            object obj = handler.Read(reader, xnb);

            if (xnb) {
                //TODO read shared resources
            }

            return obj;
        }

		public static T ReadObject<T>(BinaryReader reader, bool xnb) {
			return ReadObject<T>(reader, xnb, true);
		}

        public static T ReadObject<T>(BinaryReader reader, bool xnb, bool readPrependedData) {
            TypeHandler handler;

            string[] readerNames = null;
            TypeHandler[] handlers = null;
            int handlerIndex = -1;
            object[] sharedResources = null;
            string typeName = null;

            if (xnb && reader.BaseStream.Position == 3) {
                object[] xnbData = readXNB(reader);
                readerNames = (string[]) xnbData[0];
                handlers = (TypeHandler[]) xnbData[1];
                sharedResources = (object[]) xnbData[2];
                handler = handlers[handlerIndex = (int) xnbData[3]];
            } else {
                if (readPrependedData) {
                    if (xnb) {
                        FmbHelper.Read7BitEncodedInt(reader);
                    } else {
                        reader.ReadString();
                    }
                }
                typeName = (handler = GetTypeHandler<T>()).Type.Name;
            }

            T obj = handler.Read<T>(reader, xnb);

            if (xnb) {
                //TODO read shared resources
            }

            return obj;
        }

        public static void WriteAsset(BinaryWriter writer, object obj_) {
            writer.Write(obj_.GetType().Name);
            TypeHandler handler = GetTypeHandler(obj_.GetType().Name);
            handler.Write(writer, obj_);
        }

        private static object[] readXNB(BinaryReader reader) {
            reader.ReadByte(); //w
            reader.ReadByte(); //0x05

            byte flagBits = reader.ReadByte();
            //TODO check if 0x80 is set, if so: decompressing codepath?

            reader.ReadInt32(); //file size, optionally compressed size
            //decompressed size when compressed

            //everything from here on is compressed if 0x80 is set

            string[] readerNames = new string[FmbHelper.Read7BitEncodedInt(reader)];
            TypeHandler[] handlers = new TypeHandler[readerNames.Length];

            for (int i = 0; i < readerNames.Length; i++) {
                readerNames[i] = reader.ReadString();
                int version = reader.ReadInt32(); //reader version

                handlers[i] = GetTypeHandler(readerNames[i]);
            }

            object[] sharedResources = new object[FmbHelper.Read7BitEncodedInt(reader)];

            return new object[] { readerNames, handlers, sharedResources, FmbHelper.Read7BitEncodedInt(reader) - 1};
        }

        public static TypeHandler GetTypeHandler(string readerName) {
            TypeHandler handler;
            if (TypeHandlerReaderMap.TryGetValue(readerName, out handler)) {
                return handler;
            }

            handler = getHandler(readerName, null) ?? genHandler(readerName, null);

            TypeHandlerReaderMap[readerName] = handler;
            return handler;
        }

        public static TypeHandler GetTypeHandler<T>() {
            return GetTypeHandler(typeof(T));
        }

        public static TypeHandler GetTypeHandler(Type type) {
            TypeHandler handler;
            if (TypeHandlerTypeMap.TryGetValue(type, out handler)) {
                return handler;
            }

            handler = getHandler(null, type) ?? genHandler(null, type);

            TypeHandlerTypeMap[type] = handler;
            return handler;
        }

        private static TypeHandler getHandler(string readerName, Type type) {
            string typeName = getTypeName(readerName, type);
            string handlerName;

            //TODO check if enum, ...
            if (type == null) {
                type = FmbHelper.FindType(typeName);
            }
            if (type != null && type.IsEnum) {
                return FmbHelper.GetGenericTypeHandler(typeof(EnumHandler<>), type);
            }
            if (type != null && type.IsArray) {
                return FmbHelper.GetGenericTypeHandler(typeof(ArrayHandler<>), type.GetElementType());
            }

            //Console.WriteLine("Getting TypeHandler for " + typeName);

            Type[] types = Assembly.GetCallingAssembly().GetTypes();

            if (typeName.Contains("[[") || (type != null && false)) {
                handlerName = typeName.Substring(0, typeName.IndexOf("`")) + "Handler" + typeName.Substring(typeName.IndexOf("`"), typeName.IndexOf("[[") - typeName.IndexOf("`"));

                MatchCollection matches = GenericSplitRegex.Matches(typeName.Substring(typeName.IndexOf("[[") + 1));
                List<Type> genericParams = new List<Type>();
                for (int i = 0; i < matches.Count; i++) {
                    Match match = matches[i];
                    for (int ii = 0; ii < match.Captures.Count; ii++) {
                        string paramName = match.Captures[ii].Value;
                        paramName = paramName.Substring(1);
                        paramName = paramName.Substring(0, paramName.Length - 1);
                        if (paramName.Contains("Version=")) {
                            int commaIndex = paramName.LastIndexOf("Version=") - 1;
                            commaIndex = paramName.LastIndexOf(", ", commaIndex - 1);
                            paramName = paramName.Substring(0, commaIndex);
                        } else {
                            paramName = paramName.Substring(0, paramName.LastIndexOf(", "));
                        }
                        genericParams.Add(FmbHelper.FindType(paramName));
                    }
                }

                for (int i = 0; i < types.Length; i++) {
                    string typeName_ = getTypeName(types[i].Name, null);
                    if (typeName_ == handlerName && types[i].GetGenericArguments().Length == genericParams.Count) {
                        //Console.WriteLine("Found " + types[i].Name);
                        return (TypeHandler) types[i].MakeGenericType(genericParams.ToArray()).GetConstructor(new Type[0]).Invoke(new object[0]);
                    }
                }

                return null;
            }

            handlerName = typeName + "Handler";

            for (int i = 0; i < types.Length; i++) {
                if (getTypeName(types[i].Name, null) == handlerName) {
                    //Console.WriteLine("Found " + types[i].Name);
                    return (TypeHandler) types[i].GetConstructor(new Type[0]).Invoke(new object[0]);
                }
            }

            return null;
        }

        private static TypeHandler genHandler(string readerName, Type type) {
            TypeHandler handler = null;

            string typeName = getTypeName(readerName, type);

            //Console.WriteLine("Generating TypeHandler for " + typeName);

            Assembly assembly = Assembly.GetCallingAssembly();
            if (ManifestResourceNames == null) {
                ManifestResourceNames = assembly.GetManifestResourceNames();
            }

            string readerType = "Fallback";

            string xnapath = "FmbLib.TypeHandlerBases.Xna." + typeName + "Reader.txt";
            string fezpath = "FmbLib.TypeHandlerBases.Fez." + typeName + "Reader.txt";
            for (int i = 0; i < ManifestResourceNames.Length; i++) {
                if (xnapath == ManifestResourceNames[i]) {
                    readerType = "Xna";
                    break;
                }
                if (fezpath == ManifestResourceNames[i]) {
                    readerType = "Fez";
                    break;
                }
            }

            string usings = "using FmbLib;\nusing System;\nusing System.IO;\n";
            string reader = "";
            string writer = "";
            string readerObjectConstruction = typeName + " obj = new " + typeName + "();\n";
            string writerObjectCast = typeName + " obj = (" + typeName + ") obj_;\n";

            Console.WriteLine("Base typehandler: FmbLib.TypeHandlerBases." + readerType + "." + typeName + ".txt");

            using (Stream s = assembly.GetManifestResourceStream("FmbLib.TypeHandlerBases." + readerType + "." + typeName + "Reader.txt")) {
                if (s == null) {
                    return null;
                }
                using (StreamReader sr = new StreamReader(s)) {
                    bool usingsComplete = false;
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        if (line.StartsWith("##")) {
                            continue;
                        }

                        line = line.Trim();
                        if (line.Length == 0) {
                            continue;
                        }

                        if (!usingsComplete && line.StartsWith("using ")) {
                            usings += line;
                            usings += "\n";
                            continue;
                        }
                        usingsComplete = true;

                        if (line.StartsWith("#rc ") || line.StartsWith("#wc ")) {
                            bool read = !line.StartsWith("#w ");
                            line = line.Substring(3);
                            if (read) {
                                readerObjectConstruction = line + "\n";
                            } else {
                                writerObjectCast = line + "\n";
                            }
                        }

                        if (!line.Contains(" ") || line.StartsWith("#r ") || line.StartsWith("#w ")) {
                            bool read = !line.StartsWith("#w ");
                            if (line.StartsWith("#")) {
                                line = line.Substring(3);
                            }
                            if (!line.EndsWith(");")) {
                                line = "obj." + line + "();\n";
                            }
                            if (read) {
                                reader += line;
                            } else {
                                writer += line;
                            }
                            continue;
                        }

                        int indexSplit = line.IndexOf(" ");
                        string var = line.Substring(0, indexSplit);
                        string binaryType = line.Substring(indexSplit + 1);

                        string readingCall = "obj." + var + " = ";
                        if (binaryType.StartsWith("Object<")) {
                            readingCall += "FmbUtil.Read" + binaryType + "(reader, xnb);";
                        } else if (GeneratedTypeHandlerSpecialTypes.Contains(binaryType)) {
                            readingCall += "FmbUtil.ReadObject<" + binaryType + ">(reader, xnb, false);";
                        } else {
                            readingCall += "reader.Read" + binaryType + "();";
                        }
                        reader += readingCall;
                        reader += "\n";

                        string writingCall;
                        if (binaryType.StartsWith("Object<") || GeneratedTypeHandlerSpecialTypes.Contains(binaryType)) {
                            writingCall = "FmbUtil.WriteAsset(writer, obj." + var + ");";
                        } else {
                            writingCall = "writer.Write(obj." + var + ");";
                        }
                        writer += writingCall;
                        writer += "\n";
                    }
                }
            }

            StringBuilder builder = new StringBuilder()
                .AppendLine(usings)
                .AppendLine("public class JITTypeHandler : TypeHandler<"+typeName+"> {")
                .AppendLine()
                .AppendLine("public override object Read(BinaryReader reader, bool xnb) {")
                .AppendLine(readerObjectConstruction)
                .AppendLine(reader)
                .AppendLine("return obj;")
                .AppendLine("}")
                .AppendLine()
                .AppendLine("public override void Write(BinaryWriter writer, object obj_) {")
                .AppendLine(writerObjectCast)
                .AppendLine(writer)
                .AppendLine("}")
                .AppendLine("}");

            CompilerParameters parameters = new CompilerParameters();

			parameters.GenerateInMemory=true;
			parameters.CompilerOptions="/optimize";

            AssemblyName[] references = assembly.GetReferencedAssemblies();
            for (int i = 0; i < references.Length; i++) {
                parameters.ReferencedAssemblies.Add(references[i].Name);
            }
            parameters.ReferencedAssemblies.Add(assembly.Location);
            for (int i = 0; i < GeneratedTypeHandlerAssemblies.Count; i++) {
                string reference = GeneratedTypeHandlerAssemblies[i];
                if (parameters.ReferencedAssemblies.Contains(reference)) {
                    continue;
                }
                parameters.ReferencedAssemblies.Add(reference);
            }

            using (CSharpCodeProvider provider = new CSharpCodeProvider()) {
                try {
                    CompilerResults results = provider.CompileAssemblyFromSource(parameters, builder.ToString());

                    if (results.Errors.HasErrors) {
                        Console.WriteLine("Errors while generating TypeHandler:");
                        foreach (CompilerError error in results.Errors) {
                            Console.WriteLine(error);
                        }
                        Console.WriteLine("GeneratedTypeHandler source:");
                        Console.WriteLine(builder);
                        Console.WriteLine("Referenced assemblies:");
                        for (int i = 0; i < parameters.ReferencedAssemblies.Count; i++) {
                            Console.WriteLine(parameters.ReferencedAssemblies[i]);
                        }
                    } else {
                        Type compiledType = results.CompiledAssembly.GetType("JITTypeHandler");
                        handler = (TypeHandler) compiledType.GetConstructor(new Type[0]).Invoke(new object[0]);
                    }
                } catch (Exception e) {
                    Console.WriteLine("Error while generating TypeHandler:");
                    Console.WriteLine(e);
                    Console.WriteLine("GeneratedTypeHandler source:");
                    Console.WriteLine(builder);
                    Console.WriteLine("Referenced assemblies:");
                    for (int i = 0; i < parameters.ReferencedAssemblies.Count; i++) {
                        Console.WriteLine(parameters.ReferencedAssemblies[i]);
                    }
                }
            }

            return handler;
        }

        private static string getTypeName(string readerName, Type type) {
            string typeName;

            if (type != null) {
                typeName = type.Name;
                Type[] genericArgs = type.GetGenericArguments();
                if (genericArgs.Length != 0) {
                    typeName += "[";
                    for (int i = 0; i < genericArgs.Length; i++) {
                        typeName += "[";
                        typeName += genericArgs[i].FullName;
                        typeName += ", ";
                        typeName += genericArgs[i].Assembly.GetName().Name;
                        typeName += "]";
                        if (i != genericArgs.Length - 1) {
                            typeName += ",";
                        }
                    }
                    typeName += "]";
                }
            } else {
                typeName = readerName;
                //Console.WriteLine("Input type name: " + typeName);
                if (typeName.Contains("Readers")) {
                    Console.WriteLine("debug warning: " + typeName + " contains \"Readers\". In case of failure, debug.");
                    typeName = typeName.Replace("Readers", "Riidars");
                }
                if (typeName.Contains("[[")) {
                    //generic instantiated type
                    typeName = typeName.Substring(typeName.LastIndexOf(".", typeName.IndexOf("Reader")) + 1);
                    typeName = typeName.Replace("Reader", "");
                } else if (typeName.Contains("`")) {
                    //generic type
                    typeName = typeName.Substring(typeName.LastIndexOf(".") + 1);
                    typeName = typeName.Replace("Reader", "");
                } else if (typeName.Contains("Reader")) {
                    //xna reader
                    typeName = typeName.Substring(0, typeName.LastIndexOf("Reader"));
                    typeName = typeName.Substring(typeName.LastIndexOf(".") + 1);
                }
                //Console.WriteLine("Got type name: " + typeName);
            }

            return typeName;
        }

        public static void Main(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine("FmbLib requires one parameter: the path to the xnb.");
                Console.WriteLine("Example: FmbLib.exe level.xnb");
                return;
            }

            object obj = ReadObject(args[0]);

            Console.WriteLine("Asset type: " + obj.GetType().FullName);

            if (obj is TrileSet) {
                TrileSet ts = (TrileSet) obj;
                Console.WriteLine("TrileSet name: " + ts.Name);
                Console.WriteLine("trile count: " + ts.Triles.Count);
                foreach (Trile trile in ts.Triles.Values) {
                    Console.WriteLine(trile.Id + ": " + trile.Name);
                }
            }

        }
    }
}

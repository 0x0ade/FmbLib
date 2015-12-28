using System;
using System.IO;
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

#if UNITY
using UnityEngine;
#endif

namespace FmbLib {
    public static class FmbUtil {

        private static Regex GenericSplitRegex = new Regex(@"(\[.*?\])");

        private static char[] XNBMagic = { 'X', 'N', 'B' };

        private static Dictionary<string, TypeHandler> TypeHandlerReaderMap = new Dictionary<string, TypeHandler>();
        private static Dictionary<Type, TypeHandler> TypeHandlerTypeMap = new Dictionary<Type, TypeHandler>();
        private static string[] ManifestResourceNames;

        #if XNA
        public static bool IsXNA = true;
        #else
        public static bool IsXNA = false;
        #endif

        #if UNITY
        public static bool IsUNITY = true;
        #else
        public static bool IsUNITY = false;
        #endif

        #if FEZENGINE
        public static bool IsFEZENGINE = true;
        #else
        public static bool IsFEZENGINE = false;
        #endif

        public static bool IsTEST = false;

        static FmbUtil(){
            #if XNA || UNITY
            ____dotnetassembliesneedtobereferenced____.Add(typeof(Vector3));
            #endif
            #if FEZENGINE
            ____dotnetassembliesneedtobereferenced____.Add(typeof(TrileSet));
            #endif

            #if XNA
            GeneratedTypeHandlerAssemblies.Add("MonoGame.Framework");
            #endif
            #if FEZENGINE
            GeneratedTypeHandlerAssemblies.Add("FezEngine");
            GeneratedTypeHandlerAssemblies.Add("ContentSerialization");
            #endif

            GeneratedTypeHandlerSpecialTypes.Add("Matrix");
            GeneratedTypeHandlerSpecialTypes.Add("Quaternion");
            GeneratedTypeHandlerSpecialTypes.Add("Vector2");
            GeneratedTypeHandlerSpecialTypes.Add("Vector3");
            GeneratedTypeHandlerSpecialTypes.Add("Vector4");
            GeneratedTypeHandlerSpecialTypes.Add("Color");
            GeneratedTypeHandlerSpecialTypes.Add("BoundingSphere");

            #if UNITY
            NamespaceRemap.Add(new KeyValuePair<string, string>("Microsoft.Xna.Framework.Content", "UnityEngine"));
            NamespaceRemap.Add(new KeyValuePair<string, string>("Microsoft.Xna.Framework.Graphics", "UnityEngine"));
            NamespaceRemap.Add(new KeyValuePair<string, string>("Microsoft.Xna.Framework", "UnityEngine"));
            #endif
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

        /// <summary>
        /// List of remappings regarding the usings of generated type handlers and other pieces of namespace-sensitive code, f.e. XNA to Unity.
        /// </summary>
        public static List<KeyValuePair<string, string>> NamespaceRemap = new List<KeyValuePair<string, string>>();

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

            //string[] readerNames = null;
            TypeHandler[] handlers = null;
            //object[] sharedResources = null;

            //Console.WriteLine("Position pre xnb: " + reader.BaseStream.Position);
            if (xnb && reader.BaseStream.Position == 3) {
                object[] xnbData = readXNB(reader);
                //readerNames = (string[]) xnbData[0];
                handlers = (TypeHandler[]) xnbData[1];
                //sharedResources = (object[]) xnbData[2];
                handler = handlers[(int) xnbData[3]];
            } else if (xnb) {
                throw new InvalidOperationException("Can't read a non-asset object without type from a XNB stream!");
            } else {
                handler = GetTypeHandler(reader.ReadString());
            }

            //Console.WriteLine("Position pre main: " + reader.BaseStream.Position);
            object obj = handler.Read(reader, xnb);
            //Console.WriteLine("Position post main: " + reader.BaseStream.Position);

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

            //string[] readerNames = null;
            TypeHandler[] handlers = null;
            //object[] sharedResources = null;

            if (xnb && reader.BaseStream.Position == 3) {
                object[] xnbData = readXNB(reader);
                //readerNames = (string[]) xnbData[0];
                handlers = (TypeHandler[]) xnbData[1];
                //sharedResources = (object[]) xnbData[2];
                handler = handlers[(int) xnbData[3]];
            } else {
                handler = GetTypeHandler<T>();
                if (readPrependedData) {
                    if (xnb) {
                        int id = FmbHelper.Read7BitEncodedInt(reader);
                        //Console.WriteLine("debug: id: " + (id - 1) + ": " + handler.GetType().FullName);
                        if (id == 0) {
                            return handler.GetDefault<T>();
                        }
                    } else {
                        string type = reader.ReadString();
                        if (type == "null") {
                            return handler.GetDefault<T>();
                        }
                    }
                }
            }

            //Console.WriteLine("Position pre " + handler.Type.Name + ": " + reader.BaseStream.Position);
            T obj = handler.Read<T>(reader, xnb);
            //Console.WriteLine("Position post " + handler.Type.Name + ": " + reader.BaseStream.Position);

            if (xnb) {
                //TODO read shared resources
            }

            return obj;
        }

        public static void WriteObject(string output, object obj_) {
            if (File.Exists(output)) {
                return;
            }
            using (FileStream fos = new FileStream(output, FileMode.Create)) {
                using (BinaryWriter writer = new BinaryWriter(fos)) {
                    WriteObject(writer, obj_);
                }
            }
        }

        public static void WriteObject(BinaryWriter writer, object obj_) {
            Type type = obj_.GetType();
            writer.Write(type.Name);
            TypeHandler handler = GetTypeHandler(type);
            handler.Write(writer, obj_);
        }

        public static void WriteObject<T>(string output, T obj_) {
            if (File.Exists(output)) {
                return;
            }
            using (FileStream fos = new FileStream(output, FileMode.Create)) {
                using (BinaryWriter writer = new BinaryWriter(fos)) {
                    WriteObject<T>(writer, obj_);
                }
            }
        }

        public static void WriteObject<T>(BinaryWriter writer, T obj_) {
            if (obj_ == null) {
                writer.Write("null");
                return;
            }
            writer.Write(typeof(T).Name);
            TypeHandler handler = GetTypeHandler(typeof(T));
            handler.Write(writer, obj_);
        }

        private static object[] readXNB(BinaryReader reader) {
            reader.ReadByte(); //w
            reader.ReadByte(); //0x05

            byte flagBits = reader.ReadByte();
            if ((flagBits & 0x80) == 0x80) {
                throw new InvalidOperationException("Cannot read compressed XNBs!");
            }
            //TODO check if 0x80 is set, if so: decompressing codepath?

            reader.ReadInt32(); //file size, optionally compressed size
            //decompressed size when compressed

            //everything from here on is compressed if 0x80 is set

            string[] readerNames = new string[FmbHelper.Read7BitEncodedInt(reader)];
            TypeHandler[] handlers = new TypeHandler[readerNames.Length];

            for (int i = 0; i < readerNames.Length; i++) {
                readerNames[i] = reader.ReadString();
                reader.ReadInt32(); //reader version

                //Console.WriteLine("debug: handler: " + i + ": " + readerNames[i]);
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

            handler = getHandler(readerName, null) ?? GenerateHandler(readerName, null);

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

            handler = getHandler(null, type) ?? GenerateHandler(null, type);

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

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            //Console.WriteLine("typeName: " + typeName);

            if (typeName.Contains("[[") || (type != null && type.IsGenericType)) {
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
                        //Console.WriteLine(i + ": " + paramName + ": " + FmbHelper.FindType(paramName));
                        genericParams.Add(FmbHelper.FindType(paramName));
                    }
                }

                for (int i = 0; i < types.Length; i++) {
                    string typeName_ = getTypeName(types[i].Name, null);
                    if (typeName_ == handlerName && types[i].GetGenericArguments().Length == genericParams.Count) {
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

        public static TypeHandler GenerateHandler(string readerName, Type type) {
            TypeHandler handler = null;

            string typeName = getTypeName(readerName, type);
            if (type == null) {
                type = FmbHelper.FindType(typeName);
            }

            //Console.WriteLine("Generating TypeHandler for " + typeName);

            Assembly assembly = Assembly.GetExecutingAssembly();
            if (ManifestResourceNames == null) {
                ManifestResourceNames = assembly.GetManifestResourceNames();
            }

            string path = null;

            string comparisonReader = typeName + "Reader.txt";
            string comparisonHandler = typeName + "Handler.txt";
            for (int i = 0; i < ManifestResourceNames.Length; i++) {
                if (
                    ManifestResourceNames[i].EndsWith(comparisonReader) ||
                    ManifestResourceNames[i].EndsWith(comparisonHandler)
                ) {
                    path = ManifestResourceNames[i];
                    break;
                }
            }

            Console.WriteLine("Generating TypeHandler<" + typeName + "> from " + path);

            string source;
            using (Stream s = assembly.GetManifestResourceStream(path)) {
                if (s == null) {
                    Console.WriteLine("Resource cannot be loaded.");
                    return null;
                }
                using (StreamReader sr = new StreamReader(s)) {
                    source = GenerateHandlerSource(sr, typeName, type);
                }
            }

            CompilerParameters parameters = new CompilerParameters();

            parameters.GenerateInMemory = true;
            parameters.CompilerOptions = "/optimize";

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
                    CompilerResults results = provider.CompileAssemblyFromSource(parameters, source);

                    if (results.Errors.HasErrors) {
                        Console.WriteLine("Errors while generating TypeHandler:");
                        foreach (CompilerError error in results.Errors) {
                            Console.WriteLine(error);
                        }
                        Console.WriteLine("GeneratedTypeHandler source:");
                        Console.WriteLine(source);
                        Console.WriteLine("Referenced assemblies:");
                        for (int i = 0; i < parameters.ReferencedAssemblies.Count; i++) {
                            Console.WriteLine(parameters.ReferencedAssemblies[i]);
                        }
                    } else {
                        Type compiledType = results.CompiledAssembly.GetType("JIT" + typeName + "Handler");
                        handler = (TypeHandler) compiledType.GetConstructor(new Type[0]).Invoke(new object[0]);
                    }
                } catch (Exception e) {
                    Console.WriteLine("Error while generating TypeHandler:");
                    Console.WriteLine(e);
                    Console.WriteLine("GeneratedTypeHandler source:");
                    Console.WriteLine(source);
                    Console.WriteLine("Referenced assemblies:");
                    for (int i = 0; i < parameters.ReferencedAssemblies.Count; i++) {
                        Console.WriteLine(parameters.ReferencedAssemblies[i]);
                    }
                }
            }

            return handler;
        }

        public static string GenerateHandlerSource(StreamReader sr, string typeName, Type type) {
            return GenerateHandlerSource(sr, typeName, type, "JIT" + typeName + "Handler", null);
        }

        public static string GenerateHandlerSource(StreamReader sr, string typeName, Type type, string handlerName, string @namespace) {
            if (type == null) {
                type = FmbHelper.FindType(typeName);
            }
            if (type == null) {
                Console.WriteLine("Type not found for handler to generate: " + typeName);
            }
            string tab = @namespace == null ? "" : "\t";

            string usings = "using FmbLib;\nusing System;\nusing System.IO;\n";
            StringBuilder readerBuilder = new StringBuilder();
            StringBuilder writerBuilder = new StringBuilder();
            string readerObjectConstruction = typeName + " obj = new " + typeName + "();\n";
            string writerObjectCast = typeName + " obj = (" + typeName + ") obj_;\n";

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

                if (line.StartsWith("#if ")) {
                    line = line.Substring(4);
                    bool not = line.StartsWith("!");
                    if (not) {
                        line = line.Substring(1);
                    }
                    if (line == "XNA") {
                        line = "FmbUtil.IsXNA";
                    } else if (line == "UNITY") {
                        line = "FmbUtil.IsUNITY";
                    } else if (line == "FEZENGINE") {
                        line = "FmbUtil.IsFEZENGINE";
                    } else if (line == "TEST") {
                        line = "FmbUtil.IsTEST";
                    }
                    FmbHelper.AppendTo(tab, readerBuilder, writerBuilder);
                    FmbHelper.AppendTo("\t\t", readerBuilder, writerBuilder);
                    FmbHelper.AppendTo("if (", readerBuilder, writerBuilder);
                    if (not) {
                        FmbHelper.AppendTo("!", readerBuilder, writerBuilder);
                    }
                    FmbHelper.AppendTo(line, readerBuilder, writerBuilder);
                    FmbHelper.AppendTo(") {\n", readerBuilder, writerBuilder);
                    continue;
                }
                if (line == ("#endif")) {
                    FmbHelper.AppendTo(tab, readerBuilder, writerBuilder);
                    FmbHelper.AppendTo("\t\t}\n", readerBuilder, writerBuilder);
                    continue;
                }

                if (!usingsComplete && line.StartsWith("using ")) {
                    line = line.Substring(6, line.Length - 6 - 1);
                    for (int i = 0; i < NamespaceRemap.Count; i++) {
                        line = line.Replace(NamespaceRemap[i].Key, NamespaceRemap[i].Value);
                    }
                    line = "using " + line + ";\n";
                    if (!usings.Contains(line)) {
                        usings += line;
                    }
                    continue;
                }
                usingsComplete = true;

                if (line.StartsWith("#rc ") || line.StartsWith("#wc ")) {
                    bool read = !line.StartsWith("#wc ");
                    line = line.Substring(4);
                    if (read) {
                        readerObjectConstruction = line + "\n";
                    } else {
                        writerObjectCast = line + "\n";
                    }
                    continue;
                }

                if (!line.Contains(" ") || line.StartsWith("#r ") || line.StartsWith("#w ")) {
                    bool read = !line.StartsWith("#w ");
                    if (line.StartsWith("#")) {
                        line = line.Substring(3);
                    }
                    bool isMethod = type != null && type.GetMethod(line) != null;
                    StringBuilder lineBuilder = read ? readerBuilder : writerBuilder;
                    lineBuilder.Append(tab).Append("\t\t");
                    if (isMethod) {
                        lineBuilder.Append("obj.");
                    }
                    lineBuilder.Append(line);
                    if (isMethod) {
                        lineBuilder.Append("();");
                    }
                    lineBuilder.AppendLine();
                    continue;
                }

                FmbHelper.AppendTo(tab, readerBuilder, writerBuilder);
                FmbHelper.AppendTo("\t\t", readerBuilder, writerBuilder);

                int indexSplit = line.IndexOf(" ");
                string var = line.Substring(0, indexSplit);
                string binaryType = line.Substring(indexSplit + 1);

                readerBuilder.Append("obj.").Append(var).Append(" = ");
                if (binaryType.StartsWith("Object<")) {
                    readerBuilder.Append("FmbUtil.Read").Append(binaryType).Append("(reader, xnb);");
                } else if (GeneratedTypeHandlerSpecialTypes.Contains(binaryType)) {
                    readerBuilder.Append("FmbUtil.ReadObject<").Append(binaryType).Append(">(reader, xnb, false);");
                } else {
                    readerBuilder.Append("reader.Read").Append(binaryType).Append("();");
                }
                readerBuilder.AppendLine();

                if (binaryType.StartsWith("Object<") || GeneratedTypeHandlerSpecialTypes.Contains(binaryType)) {
                    writerBuilder.Append("FmbUtil.WriteObject(writer, obj.").Append(var);
                } else {
                    writerBuilder.Append("writer.Write(obj.").Append(var);
                }
                writerBuilder.Append(");\n");
            }

            StringBuilder builder = new StringBuilder()
                .AppendLine(usings);
            if (@namespace != null) {
                builder.Append("namespace ").Append(@namespace).AppendLine(" {");
            }
            builder.Append(tab).Append("public class ").Append(handlerName).Append(" : TypeHandler<").Append(typeName).AppendLine("> {")
                .AppendLine()
                .Append(tab).AppendLine("\tpublic override object Read(BinaryReader reader, bool xnb) {")
                .Append(tab).Append("\t\t").AppendLine(readerObjectConstruction)
                .AppendLine(readerBuilder.ToString())
                .Append(tab).AppendLine("\t\treturn obj;")
                .Append(tab).AppendLine("\t}")
                .AppendLine()
                .Append(tab).AppendLine("\tpublic override void Write(BinaryWriter writer, object obj_) {")
                .Append(tab).Append("\t\t").AppendLine(writerObjectCast)
                .Append(writerBuilder.ToString())
                .Append(tab).AppendLine("\t}")
                .Append(tab).AppendLine("}");
            if (@namespace != null) {
                builder.AppendLine("}");
            }

            return builder.ToString();
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
                    //Console.WriteLine("debug warning: " + typeName + " contains \"Readers\". In case of failure, debug.");
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

    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace FmbLib {
    public static class FmbHelper {
        
        private readonly static Dictionary<string, Type> CacheTypes = new Dictionary<string, Type>(128);

		static FmbHelper(){

			BlacklistedAssemblies.Add("SDL2-CS");
			BlacklistedAssemblies.Add("System.Drawing");

		}

        public static List<string> BlacklistedAssemblies = new List<string>(); 

        public static Type FindType(string name) {
            Type type_ = null;
            if (CacheTypes.TryGetValue(name, out type_)) {
                return type_;
            }

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Assembly> delayedAssemblies = new List<Assembly>();

            foreach (Assembly assembly in assemblies) {
                if (assembly.GetName().Name.EndsWith(".mm") || BlacklistedAssemblies.Contains(assembly.GetName().Name)) {
                    delayedAssemblies.Add(assembly);
                    continue;
                }
                try {
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types) {
                        if ((type.Name == name && type.FullName.EndsWith("."+name)) || name == type.FullName) {
                            CacheTypes[name] = type;
                            return type;
                        }
                    }
                } catch (ReflectionTypeLoadException e) {
                    Console.WriteLine("Failed searching a type in XmlHelper's FindType.");
                    Console.WriteLine("Assembly: " + assembly.GetName().Name);
                    Console.WriteLine(e.Message);
                    foreach (Exception le in e.LoaderExceptions) {
                        Console.WriteLine(le.Message);
                    }
                }
            }

            foreach (Assembly assembly in delayedAssemblies) {
                try {
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types) {
                        if ((type.Name == name && type.FullName.EndsWith("."+name)) || name == type.FullName) {
                            CacheTypes[name] = type;
                            return type;
                        }
                    }
                } catch (ReflectionTypeLoadException e) {
                    Console.WriteLine("Failed searching a type in XmlHelper's FindType.");
                    Console.WriteLine("Assembly: " + assembly.GetName().Name);
                    Console.WriteLine(e.Message);
                    foreach (Exception le in e.LoaderExceptions) {
                        Console.WriteLine(le.Message);
                    }
                }
            }

            CacheTypes[name] = null;
            return null;
        }

        public static TypeHandler GetGenericTypeHandler(Type typeHandlerType, params Type[] genericArgs) {
            return (TypeHandler) typeHandlerType.MakeGenericType(genericArgs).GetConstructor(new Type[0]).Invoke(new object[0]);
        }

        public static int Read7BitEncodedInt(BinaryReader reader) {
            int ret = 0;
            int shift = 0;
            int len;
            byte b;

            for (len = 0; len < 5; ++len) {
                b = reader.ReadByte();

                ret = ret | ((b & 0x7f) << shift);
                shift += 7;
                if ((b & 0x80) == 0) {
                    break;
                }
            }

            if (len < 5) {
                return ret;
            } else {
                throw new FormatException("Too many bytes in what should have been a 7 bit encoded Int32.");
            }
        }

    }
}


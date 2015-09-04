using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace FmbLib {
    public static class FmbHelper {
        
        private readonly static Dictionary<string, Type> CacheTypes = new Dictionary<string, Type>(128);
        private readonly static Dictionary<string, Type> CachePrefoundTypes = new Dictionary<string, Type>(1024);

        static FmbHelper() {
            BlacklistedAssemblies.Add("SDL2-CS");
            BlacklistedAssemblies.Add("System.Drawing");

            ValueTypes.Add("Matrix");
            ValueTypes.Add("Quaternion");
            ValueTypes.Add("Vector2");
            ValueTypes.Add("Vector3");
            ValueTypes.Add("Vector4");
            ValueTypes.Add("Color");
            ValueTypes.Add("BoundingSphere");
        }

        public static List<string> BlacklistedAssemblies = new List<string>();

        /// <summary>
        /// List of types that are value types in XNA but not value types in other frameworks / engines (f.e. UNITY)
        /// </summary>
        public static List<string> ValueTypes = new List<string>();

        public static Type FindType(string name) {
            return FindType_(name, false);
        }

        private static Type FindType_(string name, bool remappedNamespace) {
            Type type_ = null;
            if (CacheTypes.TryGetValue(name, out type_)) {
                return type_;
            }
            if (CachePrefoundTypes.TryGetValue(name, out type_)) {
                CacheTypes[name] = type_;
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
                        CachePrefoundTypes[type.FullName] = CachePrefoundTypes[type.Name] = type;
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

            if (!remappedNamespace) {
                string oldname = name;
                for (int i = 0; i < FmbUtil.NamespaceRemap.Count; i++) {
                    name = name.Replace(FmbUtil.NamespaceRemap[i].Key, FmbUtil.NamespaceRemap[i].Value);
                }
                Type foundType = FindType_(name, true);
                CacheTypes[oldname] = foundType;
                return foundType;
            } else {
                CacheTypes[name] = null;
                return null;
            }
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

        public static bool IsValueType(Type type) {
            return type.IsValueType || ValueTypes.Contains(type.Name);
        }

    }
}


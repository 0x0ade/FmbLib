using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;

namespace FmbLib {
    public delegate TOut SelectFunc<TIn, TOut>(TIn value);
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
                    FmbHelper.Log("Failed searching a type in XmlHelper's FindType.");
                    FmbHelper.Log("Assembly: " + assembly.GetName().Name);
                    FmbHelper.Log(e.Message);
                    foreach (Exception le in e.LoaderExceptions) {
                        FmbHelper.Log(le.Message);
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
                    FmbHelper.Log("Failed searching a type in XmlHelper's FindType.");
                    FmbHelper.Log("Assembly: " + assembly.GetName().Name);
                    FmbHelper.Log(e.Message);
                    foreach (Exception le in e.LoaderExceptions) {
                        FmbHelper.Log(le.Message);
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

        public static int Read7BitEncodedInt(this BinaryReader reader) {
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

        public static bool IsValueType(this Type type) {
            return type.IsValueType || ValueTypes.Contains(type.Name);
        }

        public static List<Type> GetGenericParamTypes(string typeName) {
            typeName = typeName.Substring(typeName.IndexOf("[[") + 1, typeName.Length - typeName.IndexOf("[[") - 2);

            List<string> matches = new List<string>();
            StringBuilder current = new StringBuilder();
            int depth = 0;
            for (int i = 0; i < typeName.Length; i++) {
                char c = typeName[i];
                if (c == '[') {
                    depth++;
                    if (depth != 1) {
                        current.Append(c);
                    }
                } else if (c == ']') {
                    depth--;
                    if (depth != 0) {
                        current.Append(c);
                    }
                } else {
                    current.Append(c);
                }
                if (depth == 0) {
                    if (1 < current.Length) {
                        matches.Add(current.ToString());
                    }
                    current = new StringBuilder();
                }
            }

            List<Type> genericParams = new List<Type>(matches.Count);
            for (int i = 0; i < matches.Count; i++) {
                string paramName = matches[i];
                if (paramName.Contains("Version=")) {
                    if (!paramName.Contains("[[")) {
                        int commaIndex = paramName.LastIndexOf("Version=") - 1;
                        commaIndex = paramName.LastIndexOf(", ", commaIndex - 1);
                        paramName = paramName.Substring(0, commaIndex);
                    } else {
                        paramName = paramName.Substring(0, paramName.LastIndexOf("]]") + 2);
                    }
                } else {
                    paramName = paramName.Substring(0, paramName.LastIndexOf(", "));
                }
                //FmbHelper.Log(i + ": " + paramName + ": " + FmbHelper.FindType(paramName));
                if (!paramName.Contains("[[")) {
                    genericParams.Add(FindType(paramName));
                } else {
                    string paramNameStrip = paramName.Substring(0, paramName.IndexOf("[["));
                    paramNameStrip = paramNameStrip.Substring(paramNameStrip.LastIndexOf('.') + 1);
                    Type type = FindType(paramNameStrip);
                    type = type.MakeGenericType(GetGenericParamTypes(paramName).ToArray());
                    genericParams.Add(type);
                }
            }
            return genericParams;
        }

#if FEZENGINE
        public static HashSet<T> HashSetOrList<T>(T[] arr, IEqualityComparer<T> comp) {
            return new HashSet<T>(arr, comp);
        }

        public static T[] HashSetOrListToArray<T>(HashSet<T> set) {
            T[] arr = new T[set.Count];
            set.CopyTo(arr);
            return arr;
        }
#else
        public static List<T> HashSetOrList<T>(T[] arr, object comp) {
            return new List<T>(arr);
        }

        public static T[] HashSetOrListToArray<T>(List<T> set) {
            return set.ToArray();
        }
#endif

        public static void AppendTo(string str, StringBuilder b1, StringBuilder b2) {
            b1.Append(str);
            b2.Append(str);
        }
        
        public static void Log(string str) {
            //This is shorter to reach than FmbUtil.Setup.Log
            if (FmbUtil.Setup.Log != null) {
                FmbUtil.Setup.Log(str);
            }
        }
        
        public static void DefaultLog(string str) {
            Console.WriteLine(str);
            #if UNITY
            UnityEngine.Debug.Log(str);
            #endif
        }
        
        // Linq
        
        // Optimized, writing to the same array
        public static T[] Select<T>(T[] a, SelectFunc<T, T> func) {
            for (int i = 0; i < a.Length; i++) {
                a[i] = func(a[i]);
            }
            return a;
        }
        
        public static TOut[] Select<TIn, TOut>(TIn[] aIn, SelectFunc<TIn, TOut> func) {
            TOut[] aOut = new TOut[aIn.Length];
            for (int i = 0; i < aIn.Length; i++) {
                aOut[i] = func(aIn[i]);
            }
            return aOut;
        }
        
        // FezMath
        
        private static readonly double Log2 = Math.Log(2D);
        public static int NextPowerOfTwo(double value) {
            return (int) Math.Pow(2D, Math.Ceiling(Math.Log(value) / Log2));
        }

        #if !UNITY
        public static float GetX(Microsoft.Xna.Framework.Vector2 v) {return v.X;}
        public static float GetX(Microsoft.Xna.Framework.Vector3 v) {return v.X;}
        public static float GetX(Microsoft.Xna.Framework.Vector4 v) {return v.X;}
        public static float GetY(Microsoft.Xna.Framework.Vector2 v) {return v.Y;}
        public static float GetY(Microsoft.Xna.Framework.Vector3 v) {return v.Y;}
        public static float GetY(Microsoft.Xna.Framework.Vector4 v) {return v.Y;}
        public static float GetZ(Microsoft.Xna.Framework.Vector3 v) {return v.Z;}
        public static float GetZ(Microsoft.Xna.Framework.Vector4 v) {return v.Z;}
        public static float GetW(Microsoft.Xna.Framework.Vector4 v) {return v.W;}
        #else
        public static float GetX(UnityEngine.Vector2 v) {return v.x;}
        public static float GetX(UnityEngine.Vector3 v) {return v.x;}
        public static float GetX(UnityEngine.Vector4 v) {return v.x;}
        public static float GetY(UnityEngine.Vector2 v) {return v.y;}
        public static float GetY(UnityEngine.Vector3 v) {return v.y;}
        public static float GetY(UnityEngine.Vector4 v) {return v.y;}
        public static float GetZ(UnityEngine.Vector3 v) {return v.z;}
        public static float GetZ(UnityEngine.Vector4 v) {return v.z;}
        public static float GetW(UnityEngine.Vector4 v) {return v.w;}
        #endif

    }
}


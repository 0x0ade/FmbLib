using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;

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

        #if FEZENGINE
        public static HashSet<T> HashSetOrList<T>(T[] arr, IEqualityComparer<T> comp) {
            return new HashSet<T>(arr, comp);
        }
        #else
        public static List<T> HashSetOrList<T>(T[] arr, object comp) {
            return new List<T>(arr);
        }
        #endif

        public static void AppendTo(string str, StringBuilder b1, StringBuilder b2) {
            b1.Append(str);
            b2.Append(str);
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

        #if UNITY
        public static UnityEngine.TextureFormat[] SurfaceFormatToTextureFormat = {
            UnityEngine.TextureFormat.ARGB32, //Color
            UnityEngine.TextureFormat.RGB565, //Bgr565 //TODO swap R and B in Texture2DHandler for Bgr565 > RGB565
            UnityEngine.TextureFormat.ARGB32, //Bgra5551 //TODO Bgra5551 is missing in Unity; Convert data!
            UnityEngine.TextureFormat.RGBA4444, //Bgra4444 //TODO swap R and B in Texture2DHandler for Bgra4444 > RGBA4444
            UnityEngine.TextureFormat.DXT1, //Dxt1
            UnityEngine.TextureFormat.ARGB32, //Dxt3 //TODO Dxt5 is missing in Unity; Convert data!
            UnityEngine.TextureFormat.DXT5, //Dxt5
            UnityEngine.TextureFormat.ARGB32, //NormalizedByte2 //TODO NormalizedByte2 is missing in Unity; Convert data!
            UnityEngine.TextureFormat.ARGB32, //NormalizedByte4 //TODO NormalizedByte4 is missing in Unity; Convert data!
            UnityEngine.TextureFormat.ARGB32, //Rgba1010102 //TODO Rgba1010102 is missing in Unity; Convert data!
            UnityEngine.TextureFormat./*RGHalf*/ ARGB32, //Rg32 //TODO Update UnityEngine assembly
            UnityEngine.TextureFormat./*RGBAHalf*/ ARGB32, //Rgba64 //TODO Update UnityEngine assembly
            UnityEngine.TextureFormat.Alpha8, //Alpha8
            UnityEngine.TextureFormat./*RFloat*/ ARGB32, //Single //TODO Update UnityEngine assembly
            UnityEngine.TextureFormat./*RGFloat*/ ARGB32, //Vector2 //TODO Update UnityEngine assembly
            UnityEngine.TextureFormat./*RGBAFloat*/ ARGB32, //Vector4 //TODO Update UnityEngine assembly
            UnityEngine.TextureFormat./*RHalf*/ ARGB32, //HalfSingle //TODO Update UnityEngine assembly
            UnityEngine.TextureFormat./*RGHalf*/ ARGB32, //HalfVector2 //TODO Update UnityEngine assembly
            UnityEngine.TextureFormat./*RGBAHalf*/ ARGB32, //HalfVector4 //TODO Update UnityEngine assembly
            UnityEngine.TextureFormat.ARGB32 //HdrBlendable //TODO HdrBlendable is missing in Unity; Convert data!
        };
        #endif

    }
}


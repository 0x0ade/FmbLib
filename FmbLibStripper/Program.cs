using System;
using Mono.Cecil;
using System.Collections.Generic;
using Mono.Cecil.Cil;

namespace FmbLib {
    internal class FmbStripper {

        private static List<string> blacklist = new List<string>() {
            "Microsoft.Xna.Framework.Graphics.VertexDeclaration.GetVertexStride", //Required for the type initializer of VertexDeclaration
            "Microsoft.Xna.Framework.Graphics.GraphicsExtensions.GetTypeSize", //Required for GetVertexStride
            "MonoGame.Utilities.Hash.ComputeHash",
            "FezEngine.Structure.Geometry.ShaderInstancedIndexedPrimitives`2.UpdateBuffers", //Required for set_Vertices and set_Indices
            "FezEngine.Structure.Geometry.IndexedPrimitiveCollectionBase`2.UpdatePrimitiveCount",

            //ToString and other possible methods are referenced in debug builds.
            //Operands may be referenced in the release builds.
            //Default values (colors, directions) are referenced.
            "Vector2",
            "Vector3",
            "Vector4",
            "Color",

            "ToString",
            "GetHashCode",
            "OnDeserialization",
            "Finalize",
            "Dispose",
            "Close"
        };

        private static void patch(ModuleDefinition module, TypeDefinition type) {
            for (int i = 0; i < type.NestedTypes.Count; i++) {
                patch(module, type.NestedTypes[i]);
            }

            if (blacklist.Contains(type.FullName) || blacklist.Contains(type.Name)) {
                return;
            }

            for (int i = 0; i < type.Methods.Count; i++) {
                MethodDefinition method = type.Methods[i];
                if (method.Name.StartsWith("get_") || method.Name.StartsWith("set_") || method.IsSpecialName || !method.HasBody || blacklist.Contains(type.FullName + "." + method.Name) || blacklist.Contains(method.Name)) {
                    continue;
                }
                method.Body.Instructions.Clear();
            }
        }

        public static void Main(string[] args) {
            foreach (string arg in args) {
                Console.WriteLine("Patching " + arg);
                ModuleDefinition module = ModuleDefinition.ReadModule(arg);

                for (int i = 0; i < module.AssemblyReferences.Count; i++) {
                    if (module.AssemblyReferences[i].Name != "mscorlib" && !module.AssemblyReferences[i].Name.StartsWith("System")) {
                        continue;
                    }
                    module.AssemblyReferences[i].Version = new Version(2, 0, 0, 0);
                }

                for (int i = 0; i < module.Types.Count; i++) {
                    patch(module, module.Types[i]);
                }

                module.Write(arg);
            }
        }
    }
}

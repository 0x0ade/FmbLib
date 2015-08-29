using System;
using FmbLib;
using System.IO;
using System.Collections.Generic;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#elif UNITY
using UnityEngine;
#else
#warning FmbLib slim XNA still WIP.
#endif

using FezEngine.Structure.Geometry;

namespace FmbLib.TypeHandlers.Fez {
#if !UNITY
    public class ShaderInstancedIndexedPrimitivesHandler<TemplateType, InstanceType> : TypeHandler<ShaderInstancedIndexedPrimitives<TemplateType, InstanceType>> where TemplateType : struct, IShaderInstantiatableVertex where InstanceType : struct {
#else
    public class ShaderInstancedIndexedPrimitivesHandler<TemplateType, InstanceType> : TypeHandler<ShaderInstancedIndexedPrimitives<TemplateType, InstanceType>> {
#endif
#if NEVER
    } //to fix MonoDevelop screwing up indentation
#endif

        public override object Read(BinaryReader reader, bool xnb) {
            PrimitiveType type = FmbUtil.ReadObject<PrimitiveType>(reader, xnb);

            ShaderInstancedIndexedPrimitives<TemplateType, InstanceType> obj = new ShaderInstancedIndexedPrimitives<TemplateType, InstanceType>(type, typeof (InstanceType) == typeof (Matrix) ? 60 : 220);
            obj.NeedsEffectCommit = true;
            obj.Vertices = FmbUtil.ReadObject<TemplateType[]>(reader, xnb);

            ushort[] indices = FmbUtil.ReadObject<ushort[]>(reader, xnb);
            obj.Indices = new int[indices.Length];
            for (int index = 0; index < indices.Length; index++) {
                obj.Indices[index] = (int) indices[index];
            }

            return obj;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            throw new NotImplementedException("Writing SIIP not implemented yet!");
        }

    }
}

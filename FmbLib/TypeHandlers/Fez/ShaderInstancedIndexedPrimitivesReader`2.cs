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
            #if !FNA
            //FEZMod integration - SIIPs in FEZ 1.12 don't seem to have the "NeedsEffectCommit" flag anymore
            obj.NeedsEffectCommit = true;
            #endif
            obj.Vertices = FmbUtil.ReadObject<TemplateType[]>(reader, xnb);

            ushort[] indices = FmbUtil.ReadObject<ushort[]>(reader, xnb);
            obj.Indices = new int[indices.Length];
            for (int i = 0; i < indices.Length; i++) {
                obj.Indices[i] = (int) indices[i];
            }

            return obj;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            ShaderInstancedIndexedPrimitives<TemplateType, InstanceType> obj = (ShaderInstancedIndexedPrimitives<TemplateType, InstanceType>) obj_;

            FmbUtil.WriteObject(writer, obj.PrimitiveType);
            FmbUtil.WriteObject(writer, obj.Vertices);
            ushort[] indices = new ushort[obj.Indices.Length];
            for (int i = 0; i < indices.Length; i++) {
                indices[i] = (ushort) obj.Indices[i];
            }
            FmbUtil.WriteObject(writer, indices);
        }

    }
}

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
    public class VertexPositionNormalTextureInstanceHandler : TypeHandler<VertexPositionNormalTextureInstance> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new VertexPositionNormalTextureInstance(FmbUtil.ReadObject<Vector3>(reader, xnb, false), reader.ReadByte(), FmbUtil.ReadObject<Vector2>(reader, xnb, false));
        }

        public override void Write(BinaryWriter writer, object obj_) {
            throw new NotImplementedException("Writing VPNTI not implemented yet!");
        }

    }
}

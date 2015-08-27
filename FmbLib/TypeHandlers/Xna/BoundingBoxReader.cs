using System;
using FmbLib;
using System.IO;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
#warning FmbLib slim XNA still WIP.
#endif

namespace FmbLib.TypeHandlers.Xna {
    public class BoundingBoxHandler : TypeHandler<BoundingBox> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new BoundingBox(FmbUtil.ReadObject<Vector3>(reader, xnb, false), FmbUtil.ReadObject<Vector3>(reader, xnb, false));
        }

        public override void Write(BinaryWriter writer, object obj_) {
            FmbUtil.WriteAsset(writer, ((BoundingBox) obj_).Min);
            FmbUtil.WriteAsset(writer, ((BoundingBox) obj_).Max);
        }
    }
}

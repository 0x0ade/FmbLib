using System;
using FmbLib;
using System.IO;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#elif UNITY
using UnityEngine;
#warning FmbLib slim XNA still WIP and no Unity BoundingBox found.
#else
#warning FmbLib slim XNA still WIP.
#endif

#if !UNITY
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
#endif

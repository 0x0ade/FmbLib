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
    public class PlaneHandler<T> : TypeHandler<Plane> {

        public override object Read(BinaryReader reader, bool xnb) {
            Plane obj = new Plane();
            obj.Normal = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
            obj.D = reader.ReadSingle();
            return obj;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            FmbUtil.WriteAsset(writer, ((Plane) obj_).Normal);
            writer.Write((float) ((Plane) obj_).D); 
        }
    }
}

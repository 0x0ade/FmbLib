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
    public class QuaternionHandler : TypeHandler<Quaternion> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new Quaternion() {
                X = reader.ReadSingle(),
                Y = reader.ReadSingle(),
                Z = reader.ReadSingle(),
                W = reader.ReadSingle()
            };
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((float) ((Quaternion) obj_).X);
            writer.Write((float) ((Quaternion) obj_).Y);
            writer.Write((float) ((Quaternion) obj_).Z);
            writer.Write((float) ((Quaternion) obj_).W);
        }
    }
}

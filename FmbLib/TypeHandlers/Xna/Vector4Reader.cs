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
    public class Vector4Handler : TypeHandler<Vector4> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new Vector4(reader.ReadSingle(),reader.ReadSingle(),reader.ReadSingle(),reader.ReadSingle());
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((float) ((Vector4) obj_).X);
            writer.Write((float) ((Vector4) obj_).Y);
            writer.Write((float) ((Vector4) obj_).Z);
            writer.Write((float) ((Vector4) obj_).W);
        }
    }
}

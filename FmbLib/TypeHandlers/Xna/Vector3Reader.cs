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
    public class Vector3Handler : TypeHandler<Vector3> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new Vector3(reader.ReadSingle(),reader.ReadSingle(),reader.ReadSingle());
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((float) ((Vector3) obj_).X);
            writer.Write((float) ((Vector3) obj_).Y);
            writer.Write((float) ((Vector3) obj_).Z);
        }
    }
}

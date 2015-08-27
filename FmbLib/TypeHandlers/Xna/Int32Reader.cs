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
    public class Int32Handler : TypeHandler<int> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((int) obj_);
        }
    }
}

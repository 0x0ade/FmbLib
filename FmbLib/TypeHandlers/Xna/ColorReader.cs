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
    public class ColorHandler : TypeHandler<Color> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new Color(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write(((Color) obj_).R);
            writer.Write(((Color) obj_).G);
            writer.Write(((Color) obj_).B);
            writer.Write(((Color) obj_).A);
        }
    }
}

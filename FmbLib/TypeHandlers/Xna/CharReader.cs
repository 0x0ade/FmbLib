using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class CharHandler : TypeHandler<char> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadChar();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((char) obj_);
        }
    }
}

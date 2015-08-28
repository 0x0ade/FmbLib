using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class Int16Handler : TypeHandler<short> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadInt16();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((short) obj_);
        }
    }
}

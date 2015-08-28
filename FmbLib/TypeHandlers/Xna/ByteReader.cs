using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class ByteHandler : TypeHandler<byte> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadByte();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((byte) obj_);
        }
    }
}

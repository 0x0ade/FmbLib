using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class UInt64Handler : TypeHandler<ulong> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadUInt64();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((ulong) obj_);
        }
    }
}

using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class UInt32Handler : TypeHandler<uint> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((uint) obj_);
        }
    }
}

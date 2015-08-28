using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class UInt16Handler : TypeHandler<ushort> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadUInt16();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((ushort) obj_);
        }
    }
}

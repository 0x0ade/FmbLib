using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class SByteHandler : TypeHandler<sbyte> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadSByte();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((sbyte) obj_);
        }
    }
}

using System;
using FmbLib;
using System.IO;

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

using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class BooleanHandler : TypeHandler<bool> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadBoolean();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((bool) obj_);
        }
    }
}

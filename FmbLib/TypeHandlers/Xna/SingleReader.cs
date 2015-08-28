using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class SingleHandler : TypeHandler<float> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((float) obj_);
        }
    }
}

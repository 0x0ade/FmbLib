using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class DoubleHandler : TypeHandler<double> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadDouble();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((double) obj_);
        }
    }
}

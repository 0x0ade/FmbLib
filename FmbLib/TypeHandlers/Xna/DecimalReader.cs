using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class DecimalHandler : TypeHandler<Decimal> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadDecimal();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((Decimal) obj_);
        }
    }
}

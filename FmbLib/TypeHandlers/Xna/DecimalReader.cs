using System;
using FmbLib;
using System.IO;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
#warning FmbLib slim XNA still WIP.
#endif

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

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
    public class DateTimeHandler : TypeHandler<DateTime> {

        public override object Read(BinaryReader reader, bool xnb) {
            //According to the docs:
            //Low 62 bits hold a .NET DateTime tick count
            //High 2 bits hold a .NET DateTimeKind enum value
            ulong kindMask = 13835058055282163712UL;
            ulong ticks = reader.ReadUInt64();
            return new DateTime((long) (ticks & ~kindMask), (DateTimeKind) ((long) (ticks >> 62) & 3L));
        }

        public override void Write(BinaryWriter writer, object obj_) {
            const ulong kindMask = 13835058055282163712UL;
            DateTime obj = (DateTime) obj_;
            //TODO test if that's enough. I'm not sure at all...
            writer.Write(((((ulong) obj.Ticks) & ~kindMask) | (((ulong) obj.Kind) << 62)));
        }
    }
}


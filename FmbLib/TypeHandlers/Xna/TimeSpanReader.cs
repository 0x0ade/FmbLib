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
    public class TimeSpanHandler : TypeHandler<TimeSpan> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new TimeSpan(reader.ReadInt64());
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write(((TimeSpan) obj_).Ticks);
        }
    }
}

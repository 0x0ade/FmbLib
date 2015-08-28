using System;
using FmbLib;
using System.IO;

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

using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class NullableHandler<T> : TypeHandler<T?> where T : struct {

        public override object Read(BinaryReader reader, bool xnb) {
            if (reader.ReadBoolean()) {
                return FmbUtil.ReadObject<T>(reader, xnb);
            } else {
                return null;
            }
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write(obj_ == null);
            if (obj_ != null) {
                FmbUtil.WriteObject(writer, obj_);
            }
        }
    }
}

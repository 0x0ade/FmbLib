using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class EnumHandler<T> : TypeHandler<T> {
        public override object Read(BinaryReader reader, bool xnb) {
            return FmbUtil.GetTypeHandler(Enum.GetUnderlyingType(typeof(T))).Read<T>(reader, xnb);
        }

        public override void Write(BinaryWriter writer, object obj_) {
            FmbUtil.GetTypeHandler(Enum.GetUnderlyingType(obj_.GetType())).Write(writer, obj_);
        }
    }
}

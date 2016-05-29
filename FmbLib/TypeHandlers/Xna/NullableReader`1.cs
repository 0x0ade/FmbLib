using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class NullableHandler<T> : TypeHandler<T?> where T : struct {

        public override object Read(BinaryReader reader, bool xnb) {
            if (/*reader.ReadBoolean()*/ true) {
                // FIXME this should read a boolean, but reading it kills everything by one byte...
                // It's possible that reading boolean means simply checking if ID > 0, but that's already happening in FmbUtil
                return FmbUtil.GetTypeHandler(typeof(T)).Read<T>(reader, xnb);
            }
            //return null;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            // writer.Write(obj_ != null); // FIXME this should read a boolean, but writing it kills reading
            if (obj_ != null) {
                FmbUtil.GetTypeHandler(typeof(T)).Write(writer, obj_);
            }
        }
        
        public override object GetDefault() {
            return new T?();
        }
    }
}

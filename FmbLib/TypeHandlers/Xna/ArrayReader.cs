using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class ArrayHandler<T> : TypeHandler<T[]> {
        
        public override object Read(BinaryReader reader, bool xnb) {
            T[] obj = new T[(int) ((IntPtr) reader.ReadUInt32())];

            if (typeof(T).IsValueType() || !xnb) {
                for (int i = 0; i < obj.Length; i++) {
                    obj[i] = FmbUtil.ReadObject<T>(reader, xnb, false);
                }
            } else {
                for (int i = 0; i < obj.Length; i++) {
                    int readerIndex = reader.Read7BitEncodedInt(); //FmbLib ain't no care about reader index.
                    obj[i] = readerIndex > 0 ? FmbUtil.ReadObject<T>(reader, xnb, false) : default(T);
                }
            }

            return obj;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            T[] obj = (T[]) obj_;
            TypeHandler handler = FmbUtil.GetTypeHandler(typeof(T));

            writer.Write((uint) obj.Length);

            for (int i = 0; i < obj.Length; i++) {
                handler.Write(writer, obj[i]);
            }
        }
    }
}

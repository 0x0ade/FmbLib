using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class ArrayHandler<T> : TypeHandler<T[]> {
        
        public override object Read(BinaryReader reader, bool xnb) {
            T[] obj = new T[(int) ((IntPtr) reader.ReadUInt32())];

            if (FmbHelper.IsValueType(typeof(T)) || !xnb) {
                for (int i = 0; i < obj.Length; i++) {
                    obj[i] = FmbUtil.ReadObject<T>(reader, xnb, false);
                }
            } else {
                for (int i = 0; i < obj.Length; i++) {
                    int readerIndex = FmbHelper.Read7BitEncodedInt(reader); //FmbLib ain't no care about reader index.
                    obj[i] = readerIndex > 0 ? FmbUtil.ReadObject<T>(reader, xnb, false) : default(T);
                }
            }

            return obj;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            T[] obj = (T[]) obj_;

            writer.Write((uint) obj.Length);

            for (int i = 0; i < obj.Length; i++) {
                FmbUtil.WriteObject(writer, obj[i]);
            }
        }
    }
}

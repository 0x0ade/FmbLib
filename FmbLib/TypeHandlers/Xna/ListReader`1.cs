using System;
using FmbLib;
using System.IO;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Xna {
    public class ListHandler<T> : TypeHandler<List<T>> {

        public override object Read(BinaryReader reader, bool xnb) {
            Type type = typeof(T);
            TypeHandler handler = FmbUtil.GetTypeHandler(type);

            int capacity = reader.ReadInt32();
            List<T> obj = new List<T>(capacity);
            for (int i = 0; i < capacity; i++) {
                if (FmbHelper.IsValueType(typeof(T)) || !xnb) {
                    obj.Add(handler.Read<T>(reader, xnb));
                } else {
                    int readerIndex = reader.ReadByte(); //FmbLib ain't no care about reader index.
                    obj.Add(readerIndex > 0 ? handler.Read<T>(reader, xnb) : default(T));
                }
            }

            return obj;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            List<T> obj = (List<T>) obj_;

            writer.Write((byte) obj.Count);

            for (int i = 0; i < obj.Count; i++) {
                FmbUtil.WriteAsset(writer, obj[i]);
            }
        }
    }
}


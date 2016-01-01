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
            #if DEBUG
            Console.WriteLine("T: " + type.FullName);
            Console.WriteLine("Capacity: " + capacity);
            List<T> obj = new List<T>(0);
            #else
            List<T> obj = new List<T>(capacity);
            #endif
            bool isValueType = FmbHelper.IsValueType(type);
            for (int i = 0; i < capacity; i++) {
                if (isValueType || !xnb) {
                    obj.Add(handler.Read<T>(reader, xnb));
                } else {
                    int readerIndex = FmbHelper.Read7BitEncodedInt(reader); //FmbLib ain't no care about reader index.
                    obj.Add(readerIndex > 0 ? handler.Read<T>(reader, xnb) : default(T));
                }
            }

            return obj;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            List<T> obj = (List<T>) obj_;
            TypeHandler handler = FmbUtil.GetTypeHandler(typeof(T));

            writer.Write((int) obj.Count);

            for (int i = 0; i < obj.Count; i++) {
                handler.Write(writer, obj[i]);
            }
        }
    }
}


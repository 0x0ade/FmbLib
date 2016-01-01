using System;
using FmbLib;
using System.IO;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Xna {
    public class DictionaryHandler<TKey, TValue> : TypeHandler<Dictionary<TKey, TValue>> {

        public override object Read(BinaryReader reader, bool xnb) {
            Type keyType = typeof(TKey);
            Type valueType = typeof(TValue);
            TypeHandler keyHandler = FmbUtil.GetTypeHandler(keyType);
            TypeHandler valueHandler = FmbUtil.GetTypeHandler(valueType);

            int capacity = reader.ReadInt32();
            #if DEBUG
            Console.WriteLine("TKey: " + keyType.FullName);
            Console.WriteLine("TValue: " + valueType.FullName);
            Console.WriteLine("Capacity: " + capacity);
            Dictionary<TKey, TValue> obj = new Dictionary<TKey, TValue>(0);
            #else
            Dictionary<TKey, TValue> obj = new Dictionary<TKey, TValue>(capacity);
            #endif
            bool keyIsValueType = FmbHelper.IsValueType(keyType);
            bool valueIsValueType = FmbHelper.IsValueType(valueType);
            for (int i = 0; i < capacity; i++) {
                TKey key;
                if (keyIsValueType || !xnb) {
                    key = keyHandler.Read<TKey>(reader, xnb);
                } else {
                    int readerIndex = reader.ReadByte(); //FmbLib ain't no care about reader index.
                    key = readerIndex > 0 ? keyHandler.Read<TKey>(reader, xnb) : default(TKey);
                }
                TValue value;
                    if (valueIsValueType || !xnb) {
                    value = valueHandler.Read<TValue>(reader, xnb);
                } else {
                    int readerIndex = reader.ReadByte(); //FmbLib ain't no care about reader index.
                    value = readerIndex > 0 ? valueHandler.Read<TValue>(reader, xnb) : default(TValue);
                }
                obj.Add(key, value);
            }

            return obj;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            Dictionary<TKey, TValue> obj = (Dictionary<TKey, TValue>) obj_;
            TypeHandler keyHandler = FmbUtil.GetTypeHandler(typeof(TKey));
            TypeHandler valueHandler = FmbUtil.GetTypeHandler(typeof(TValue));

            writer.Write((int) obj.Count);

            foreach (KeyValuePair<TKey, TValue> pair in obj) {
                keyHandler.Write(writer, pair.Key);
                valueHandler.Write(writer, pair.Value);
            }
        }

    }
}

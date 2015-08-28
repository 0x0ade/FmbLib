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
            Dictionary<TKey, TValue> obj = new Dictionary<TKey, TValue>(capacity);
            for (int i = 0; i < capacity; i++) {
                TKey key;
                if (keyType.IsValueType || !xnb) {
                    key = keyHandler.Read<TKey>(reader, xnb);
                } else {
                    int readerIndex = reader.ReadByte(); //FmbLib ain't no care about reader index.
                    key = readerIndex > 0 ? keyHandler.Read<TKey>(reader, xnb) : default(TKey);
                }
                TValue value;
                if (valueType.IsValueType || !xnb) {
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

            writer.Write((byte) obj.Count);

            TKey[] keys = new TKey[obj.Count];
            obj.Keys.CopyTo(keys, 0);
            TValue[] values = new TValue[obj.Count];
            obj.Values.CopyTo(values, 0);

            for (int i = 0; i < obj.Count; i++) {
                FmbUtil.WriteAsset(writer, keys[i]);
                FmbUtil.WriteAsset(writer, values[i]);
            }
        }

    }
}

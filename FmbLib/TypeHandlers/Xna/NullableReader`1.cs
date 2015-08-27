using System;
using FmbLib;
using System.IO;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
#warning FmbLib slim XNA still WIP.
#endif

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
                FmbUtil.WriteAsset(writer, obj_);
            }
        }
    }
}

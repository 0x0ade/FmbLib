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
    public class EnumHandler<T> : TypeHandler<T> {
        public override object Read(BinaryReader reader, bool xnb) {
            return FmbUtil.GetTypeHandler(Enum.GetUnderlyingType(typeof(T))).Read<T>(reader, xnb);
        }

        public override void Write(BinaryWriter writer, object obj_) {
            throw new NotImplementedException("Writing enums not implemented yet!");
        }
    }
}

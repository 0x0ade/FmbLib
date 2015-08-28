using System;
using FmbLib;
using System.IO;

namespace FmbLib.TypeHandlers.Xna {
    public class StringHandler : TypeHandler<string> {

        public override object Read(BinaryReader reader, bool xnb) {
            return reader.ReadString();
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((string) obj_);
        }
    }
}

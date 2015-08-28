using System;
using FmbLib;
using System.IO;

#if !UNITY
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
using UnityEngine;
#endif

namespace FmbLib.TypeHandlers.Xna {
    public class RectangleHandler : TypeHandler<Rectangle> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((int) ((Rectangle) obj_).X);
            writer.Write((int) ((Rectangle) obj_).Y);
            writer.Write((int) ((Rectangle) obj_).Width);
            writer.Write((int) ((Rectangle) obj_).Height);
        }
    }
}

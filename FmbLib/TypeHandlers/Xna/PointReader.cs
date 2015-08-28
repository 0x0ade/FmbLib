using System;
using FmbLib;
using System.IO;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#elif UNITY
using UnityEngine;
#warning FmbLib slim XNA still WIP and no Unity Point found.
#else
#warning FmbLib slim XNA still WIP.
#endif

#if !UNITY
namespace FmbLib.TypeHandlers.Xna {
    public class PointHandler : TypeHandler<Point> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new Point(reader.ReadInt32(), reader.ReadInt32());
        }

        public override void Write(BinaryWriter writer, object obj_) {
            writer.Write((int) ((Point) obj_).X);
            writer.Write((int) ((Point) obj_).Y);
        }
    }
}
#endif

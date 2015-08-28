using System;
using FmbLib;
using System.IO;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#elif UNITY
using UnityEngine;
#else
#warning FmbLib slim XNA still WIP.
#endif

namespace FmbLib.TypeHandlers.Xna {
    public class QuaternionHandler : TypeHandler<Quaternion> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public override void Write(BinaryWriter writer, object obj_) {
            #if !UNITY
            writer.Write((float) ((Quaternion) obj_).X);
            writer.Write((float) ((Quaternion) obj_).Y);
            writer.Write((float) ((Quaternion) obj_).Z);
            writer.Write((float) ((Quaternion) obj_).W);
            #else
            writer.Write((float) ((Quaternion) obj_).x);
            writer.Write((float) ((Quaternion) obj_).y);
            writer.Write((float) ((Quaternion) obj_).z);
            writer.Write((float) ((Quaternion) obj_).w);
            #endif
        }
    }
}

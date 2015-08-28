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
    public class Vector3Handler : TypeHandler<Vector3> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public override void Write(BinaryWriter writer, object obj_) {
            #if !UNITY
            writer.Write((float) ((Vector3) obj_).X);
            writer.Write((float) ((Vector3) obj_).Y);
            writer.Write((float) ((Vector3) obj_).Z);
            #else
            writer.Write((float) ((Vector3) obj_).x);
            writer.Write((float) ((Vector3) obj_).y);
            writer.Write((float) ((Vector3) obj_).z);
            #endif
        }
    }
}

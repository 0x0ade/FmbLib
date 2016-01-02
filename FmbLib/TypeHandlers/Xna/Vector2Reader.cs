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
    public class Vector2Handler : TypeHandler<Vector2> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new Vector2(reader.ReadSingle(), reader.ReadSingle());
		}

        public override void Write(BinaryWriter writer, object obj_) {
            if (obj_ is Vector2) {
                #if !UNITY
                writer.Write((float) ((Vector2) obj_).X);
                writer.Write((float) ((Vector2) obj_).Y);
                #else
                writer.Write((float) ((Vector2) obj_).x);
                writer.Write((float) ((Vector2) obj_).y);
                #endif
            } else if (obj_ is Vector3) {
                #if !UNITY
                writer.Write((float) ((Vector3) obj_).X);
                writer.Write((float) ((Vector3) obj_).Y);
                #else
                writer.Write((float) ((Vector3) obj_).x);
                writer.Write((float) ((Vector3) obj_).y);
                #endif
            } else if (obj_ is Vector4) {
                #if !UNITY
                writer.Write((float) ((Vector4) obj_).X);
                writer.Write((float) ((Vector4) obj_).Y);
                #else
                writer.Write((float) ((Vector4) obj_).x);
                writer.Write((float) ((Vector4) obj_).y);
                #endif
            }
        }
    }
}

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
    public class PlaneHandler<T> : TypeHandler<Plane> {

        public override object Read(BinaryReader reader, bool xnb) {
            Plane obj = new Plane();
            #if !UNITY
            obj.Normal = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
            obj.D = reader.ReadSingle();
            #else
            obj.normal = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
            obj.distance = reader.ReadSingle();
            #endif
            return obj;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            #if !UNITY
            FmbUtil.WriteObject(writer, ((Plane) obj_).Normal);
            writer.Write((float) ((Plane) obj_).D);
            #else
            FmbUtil.WriteObject(writer, ((Plane) obj_).normal);
            writer.Write((float) ((Plane) obj_).distance);
            #endif
        }
    }
}

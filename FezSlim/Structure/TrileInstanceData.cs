#if !FEZENGINE
using System;
using System.Collections;
using System.Collections.Generic;
using FezEngine.Structure.Geometry;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#elif UNITY
using UnityEngine;
#else
#warning FmbLib slim XNA still WIP.
#endif

namespace FezEngine.Structure {
    public struct TrileInstanceData {

        public Vector4 PositionPhi;

        public static int SizeInBytes {
            get {
                return 16;
            }
        }

        public TrileInstanceData(Vector3 position, float phi) {
            #if !UNITY
            PositionPhi = new Vector4(position, phi);
            #else
            PositionPhi = new Vector4(position.x, position.y, position.z, phi);
            #endif
        }

        public override string ToString() {
            return string.Format("{{PositionPhi:{0}}}", PositionPhi);
        }

        public bool Equals(TrileInstanceData other) {
            return other.PositionPhi.Equals(PositionPhi);
        }

    }
}
#endif

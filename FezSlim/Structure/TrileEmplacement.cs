#if !FEZENGINE
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
    public struct TrileEmplacement {

        public int X;
        public int Y;
        public int Z;

        public TrileEmplacement(Vector3 position) {
            #if !UNITY
            X = Mathf.RoundToInt(position.X);
            Y = Mathf.RoundToInt(position.Y);
            Z = Mathf.RoundToInt(position.Z);
            #else
            X = Mathf.RoundToInt(position.x);
            Y = Mathf.RoundToInt(position.y);
            Z = Mathf.RoundToInt(position.z);
            #endif
        }
        
        public TrileEmplacement(int x, int y, int z)  {
            X = x;
            Y = y;
            Z = z;
        }
    	
    }
}
#endif

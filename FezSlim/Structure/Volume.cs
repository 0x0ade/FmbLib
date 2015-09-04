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
    public class Volume {
        
        public int Id;
        public bool Enabled;
        public bool PlayerInside;
        public bool? PlayerIsHigher;
        public List<FaceOrientation> Orientations; //Originally HashSet, but it doesn't exist in .NET 2.0
        public VolumeActorSettings ActorSettings;
        public Vector3 From;
        public Vector3 To;
        public BoundingBox BoundingBox {
            get {
                return new BoundingBox(From, To);
            }
        }

        public Volume() {
            Orientations = new List<FaceOrientation>();
            Enabled = true;
        }
        
    }
}
#endif

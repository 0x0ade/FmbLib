#if !FEZENGINE
using System.Collections;
using System.Collections.Generic;
using FezEngine;
using FezEngine.Structure.Geometry;
using FezEngine.Structure.Input;

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
    public class MovementPath {

        public int Id;
        public bool IsSpline;
        public float OffsetSeconds;
        public List<PathSegment> Segments;
        public PathEndBehavior EndBehavior;
        public bool NeedsTrigger;
        public string SoundName;
        public bool RunOnce;
        public bool RunSingleSegment;
        public bool Backwards;
        public bool InTransition;
        public bool OutTransition;
        public bool SaveTrigger;
        
        public MovementPath() {
            Segments = new List<PathSegment>();
        }

    }
}
#endif

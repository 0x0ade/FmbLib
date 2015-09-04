#if !FEZENGINE
using System;
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
    public class PathSegment {

        public Vector3 Destination;
        
        public Quaternion Orientation;
        
        public TimeSpan WaitTimeOnStart;
        
        public TimeSpan WaitTimeOnFinish;
        
        public TimeSpan Duration;
        
        public float Acceleration;
        
        public float Deceleration;
        
        public float JitterFactor;
        
        public bool Bounced;
        
        //public SoundEffect Sound; //TODO
        
        public ICloneable CustomData;
        
        public PathSegment() {
            Duration = TimeSpan.FromSeconds(1.0);
            #if !UNITY
            Orientation = Quaternion.Identity;
            #else
            Orientation = Quaternion.identity;
            #endif
        }

    }
}
#endif

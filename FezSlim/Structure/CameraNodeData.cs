#if !FEZENGINE
using System.Collections;
using System.Collections.Generic;
using FezEngine.Structure.Geometry;
using System;

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
    public class CameraNodeData : ICloneable {
        
        public bool Perspective;
        public int PixelsPerTrixel;
        public string SoundName;
        
        public object Clone() {
            return new CameraNodeData() {
                Perspective = this.Perspective,
                PixelsPerTrixel = this.PixelsPerTrixel,
                SoundName = this.SoundName
            };
        }
        
    }
}
#endif

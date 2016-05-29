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
			CameraNodeData cnd = new CameraNodeData ();
			cnd.Perspective = this.Perspective;
			cnd.PixelsPerTrixel = this.PixelsPerTrixel;
			cnd.SoundName = this.SoundName;
			return cnd;
        }
        
    }
}
#endif

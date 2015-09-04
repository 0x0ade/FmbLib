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
    public class TrileFace {
        
        public TrileEmplacement Id;
        public FaceOrientation Face;

        public TrileFace() {
            Id = new TrileEmplacement();
        }
        
    }
}
#endif

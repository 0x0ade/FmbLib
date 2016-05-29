#if !FEZENGINE
using System;
using System.Collections;
using System.Collections.Generic;
using FezEngine.Structure.Geometry;
using FezEngine.Tools;

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
    public class AnimatedTexture : IDisposable {
        public Texture2D Texture;
        public Rectangle[] Offsets;
        public AnimationTiming Timing;
        public int FrameWidth;
        public int FrameHeight;
        public Vector2 PotOffset;
        public bool NoHat;
    
        public void Dispose() {
            if (Texture != null) {
                //TextureExtensions.Unhook(Texture);
                #if UNITY
                GameObject.Destroy(Texture);
                #else
                Texture.Dispose();
                #endif
            }
            Texture = null;
            Timing = null;
        }
    }
}
#endif

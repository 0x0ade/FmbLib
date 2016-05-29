#if !FEZENGINE
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

namespace FezEngine.Content {
    public class AnimatedTextureContent {
        public readonly List<FrameContent> Frames = new List<FrameContent>();
        public int FrameWidth;
        public int FrameHeight;
        public int Width;
        public int Height;
        public byte[] PackedImage;
    }
}
#endif

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
    public class SkyLayer {

        public string Name;
        public bool InFront;
        public float Opacity = 1f;
        public float FogTint;

        public SkyLayer ShallowCopy() {
            SkyLayer layer = new SkyLayer();
            layer.UpdateFromCopy(this);
            return layer;
        }

        public void UpdateFromCopy(SkyLayer copy) {
            Name = copy.Name;
            InFront = copy.InFront;
            Opacity = copy.Opacity;
            FogTint = copy.FogTint;
        }

    }
}
#endif

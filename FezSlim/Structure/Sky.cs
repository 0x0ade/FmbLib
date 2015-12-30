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
    public class Sky {

        public string Name = "Default";
        public float WindSpeed = 1f;
        public float Density = 1f;
        public float FogDensity = 0.02f;
        public List<string> Clouds = new List<string>();
        public List<SkyLayer> Layers = new List<SkyLayer>();
        public string Background = "SkyBack";
        public string Shadows;
        public string Stars;
        public string CloudTint;
        public bool VerticalTiling;
        public bool HorizontalScrolling;
        public float InterLayerVerticalDistance;
        public float InterLayerHorizontalDistance;
        public float HorizontalDistance;
        public float VerticalDistance;
        public float LayerBaseHeight = 0.5f;
        public float LayerBaseSpacing;
        public float WindParallax;
        public float WindDistance;
        public float CloudsParallax = 1f;
        public float ShadowOpacity = 0.7f;
        public bool FoliageShadows;
        public bool NoPerFaceLayerXOffset;
        public float LayerBaseXOffset;

        public Sky ShallowCopy() {
            Sky sky = new Sky();
            sky.UpdateFromCopy(this);
            return sky;
        }

        public void UpdateFromCopy(Sky copy) {
            Name = copy.Name;
            Background = copy.Background;
            Clouds = new List<string>(copy.Clouds);
            Layers = new List<SkyLayer>(copy.Layers);
            for (int i = 0; i < Layers.Count; i++) {
                Layers[i] = Layers[i].ShallowCopy();
            }
            WindSpeed = copy.WindSpeed;
            Density = copy.Density;
            Shadows = copy.Shadows;
            Stars = copy.Stars;
            FogDensity = copy.FogDensity;
            VerticalTiling = copy.VerticalTiling;
            HorizontalScrolling = copy.HorizontalScrolling;
            InterLayerVerticalDistance = copy.InterLayerVerticalDistance;
            InterLayerHorizontalDistance = copy.InterLayerHorizontalDistance;
            HorizontalDistance = copy.HorizontalDistance;
            VerticalDistance = copy.VerticalDistance;
            LayerBaseHeight = copy.LayerBaseHeight;
            LayerBaseSpacing = copy.LayerBaseSpacing;
            WindParallax = copy.WindParallax;
            WindDistance = copy.WindDistance;
            CloudsParallax = copy.CloudsParallax;
            FoliageShadows = copy.FoliageShadows;
            ShadowOpacity = copy.ShadowOpacity;
            NoPerFaceLayerXOffset = copy.NoPerFaceLayerXOffset;
            LayerBaseXOffset = copy.LayerBaseXOffset;
        }

    }
}
#endif

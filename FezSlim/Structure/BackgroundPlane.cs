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
    public class BackgroundPlane {
        
        public int Id;
        public Group Group;
        public Mesh HostMesh;
        public ActorType ActorType;
        public int InstanceIndex;
        public bool Visible;
        public bool Hidden;
        public bool Animated;
        public bool Billboard;
        public bool SyncWithSamples;
        //public AnimationTiming Timing; //TODO
        public bool Loop;
        public Vector3 Forward;
        public Vector3? OriginalPosition;
        public Quaternion OriginalRotation;
        public Vector3 Position;
        public FaceOrientation Orientation;
        public Quaternion Rotation = 
            #if !UNITY
            Quaternion.Identity;
            #else
            Quaternion.identity;
            #endif
        public Vector3 Scale;
        public Vector3 Size;
        public string TextureName;
        public Texture Texture;
        public float Opacity = 1f;
        public float ParallaxFactor;
        public bool LightMap;
        public bool AllowOverbrightness;
        public bool Doublesided;
        public bool Crosshatch;
        public int? AttachedGroup;
        public int? AttachedPlane;
        public Color Filter =
            #if !UNITY
            Color.White;
            #else
            Color.white;
            #endif
        public bool AlwaysOnTop;
        public bool Fullbright;
        public bool PixelatedLightmap;
        public bool ClampTexture;
        public bool XTextureRepeat;
        public bool YTextureRepeat;

        public BackgroundPlane() {
            Loop = true;
            Visible = true;
            Orientation = FaceOrientation.Front;
            #if !UNITY
            OriginalRotation = Quaternion.Identity;
            #else
            OriginalRotation = Quaternion.identity;
            #endif
        }

    }
}
#endif

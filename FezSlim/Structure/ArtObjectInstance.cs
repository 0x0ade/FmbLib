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
    public class ArtObjectInstance {

        public int Id;
        public bool Enabled = true;
        public bool Hidden;
        public bool Visible = true;
        public int InstanceIndex;
        public ArtObjectActorSettings ActorSettings;
        public Vector3 Position;
        public Vector3 Scale = 
            #if !UNITY
            Vector3.One;
            #else
            Vector3.one;
            #endif
        public Quaternion Rotation = 
            #if !UNITY
            Quaternion.Identity;
            #else
            Quaternion.identity;
            #endif
        public Material Material;
        public bool ForceShading;
        public string ArtObjectName;
        public ArtObject ArtObject;

        public ArtObjectInstance() {
            ActorSettings = new ArtObjectActorSettings();
            Enabled = true;
        }
    	
    }
}
#endif

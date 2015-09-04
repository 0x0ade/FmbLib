#if !FEZENGINE
using System.Collections;
using System.Collections.Generic;
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
    public class VolumeActorSettings {

        public List<DotDialogueLine> DotDialogue = new List<DotDialogueLine>();
        public int NextLine = -1;
        public Vector2 FarawayPlaneOffset;
        public float WaterOffset;
        public string DestinationSong;
        public float DestinationPixelsPerTrixel;
        public float DestinationRadius;
        public Vector2 DestinationOffset;
        public bool IsPointOfInterest;
        public bool WaterLocked;
        public bool IsSecretPassage;
        public CodeInput[] CodePattern;
        public bool IsBlackHole;
        public bool Sucking;
        public bool NeedsTrigger;
        public bool PreventHey;

    }
}
#endif

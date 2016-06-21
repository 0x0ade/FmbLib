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
    public class NpcMetadata {
        public float WalkSpeed = 1.5f;
        public List<NpcAction> SoundActions = new List<NpcAction>();
        public bool AvoidsGomez;
        public ActorType ActorType;
        public string SoundPath;
    }
}
#endif

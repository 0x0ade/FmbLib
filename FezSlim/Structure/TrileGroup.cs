#if !FEZENGINE
using System.Collections;
using System.Collections.Generic;

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
    public class TrileGroup {

        public int Id;
        public string Name;
        public List<TrileInstance> Triles;
        public MovementPath Path;
        public bool Heavy;
        public ActorType ActorType;
        public bool InMidAir;
        public float GeyserOffset;
        public float GeyserPauseFor;
        public float GeyserLiftFor;
        public float GeyserApexHeight;
        public bool MoveToEnd;
        public bool Spin180Degrees;
        public bool SpinClockwise;
        public float SpinFrequency;
        public bool SpinNeedsTriggering;
        public Vector3 SpinCenter;
        public bool FallOnRotate;
        public float SpinOffset;
        public string AssociatedSound;
        public bool PhysicsInitialized;

        public TrileGroup() {
            Name = "Unnamed";
            Triles = new List<TrileInstance>();
        }

    }
}
#endif

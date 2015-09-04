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
    public class TrileInstance {

        private static readonly Quaternion[] QuatLookup = new Quaternion[4] {
            new Quaternion(0.0f, (float) Mathf.Sin(-3.141593f / 2f), 0.0f, (float) Mathf.Cos(-3.141593f / 2f)),
            new Quaternion(0.0f, (float) Mathf.Sin(-1.570796f / 2f), 0.0f, (float) Mathf.Cos(-1.570796f / 2f)),
            new Quaternion(0.0f, (float) Mathf.Sin(-0.0f / 2f), 0.0f, (float) Mathf.Cos(0.0f / 2f)),
            new Quaternion(0.0f, (float) Mathf.Sin(1.570796f / 2f), 0.0f, (float) Mathf.Cos(1.570796f / 2f)),
        };
        private static readonly FaceOrientation[] OrientationLookup = new FaceOrientation[4] {
            FaceOrientation.Back,
            FaceOrientation.Left,
            FaceOrientation.Front,
            FaceOrientation.Right
        };

        private Quaternion phiQuat;
        private FaceOrientation phiOri;

        public Trile Trile;
        public Trile VisualTrile;

        public int TrileId;
        public InstanceActorSettings ActorSettings;

        public float Phi;

        public List<TrileInstance> OverlappedTriles = new List<TrileInstance>();

        public Vector3 Position;

        public int? VisualTrileId;
        public int InstanceId;
        public bool Enabled;
        public bool Removed;
        public bool Collected;
        public bool Hidden;
        public bool Foreign;
        public bool ForceSeeThrough;
        public bool ForceTopMaybe;
        public bool SkipCulling;
        public bool NeedsRandomCleanup;
        public float LastTreasureSin;

        //public InstancePhysicsState PhysicsState; //TODO

        public TrileEmplacement Emplacement;

        //public TrileInstanceData Data; //TODO

        public bool IsMovingGroup;
        public TrileEmplacement OriginalEmplacement;
        public bool Unsafe;
        #if !UNITY
        public Point? OldSsEmplacement;
        #else
        public Point OldSsEmplacement;
        #endif

        public TrileInstance() {
            ActorSettings = new InstanceActorSettings();
            Enabled = true;
            InstanceId = -1;
        }

        public void SetPhiLight(byte orientation) {
            //Data.PositionPhi.W = (float) ((int) orientation - 2) * 1.570796f; //TODO
            phiQuat = TrileInstance.QuatLookup[(int) orientation];
            phiOri = TrileInstance.OrientationLookup[(int) orientation];
        }
    	
    }
}
#endif

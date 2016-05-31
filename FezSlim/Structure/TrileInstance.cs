#if !FEZENGINE
using System;
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

        //For some reason, Mathf hates me.
        private static readonly Quaternion[] QuatLookup = new Quaternion[4] {
            new Quaternion(0.0f, (float) Math.Sin(-3.141593f / 2f), 0.0f, (float) Math.Cos(-3.141593f / 2f)),
            new Quaternion(0.0f, (float) Math.Sin(-1.570796f / 2f), 0.0f, (float) Math.Cos(-1.570796f / 2f)),
            new Quaternion(0.0f, (float) Math.Sin(-0.0f / 2f), 0.0f, (float) Math.Cos(0.0f / 2f)),
            new Quaternion(0.0f, (float) Math.Sin(1.570796f / 2f), 0.0f, (float) Math.Cos(1.570796f / 2f)),
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

        public float Phi {
            get {
                #if !UNITY
                return Data.PositionPhi.W;
                #else
                return Data.PositionPhi.w;
                #endif
            }
            set {
                #if !UNITY
                Data.PositionPhi.W = value;
                #else
                Data.PositionPhi.w = value;
                #endif
            }
        }

        public List<TrileInstance> OverlappedTriles = new List<TrileInstance>();

        private Vector3 cachedPosition;
        public Vector3 Position {
            get {
                return cachedPosition;
            }
            set {
                #if !UNITY
                Data.PositionPhi.X = value.X;
                Data.PositionPhi.Y = value.Y;
                Data.PositionPhi.Z = value.Z;
                #else
                Data.PositionPhi.x = value.x;
                Data.PositionPhi.y = value.y;
                Data.PositionPhi.z = value.z;
                #endif
                cachedPosition = value;
                cachedEmplacement = new TrileEmplacement(cachedPosition);
            }
        }

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

        private TrileEmplacement cachedEmplacement;
        public TrileEmplacement Emplacement {
            get {
                return cachedEmplacement;
            }
            set {
                #if !UNITY
                Data.PositionPhi.X = value.X;
                Data.PositionPhi.Y = value.Y;
                Data.PositionPhi.Z = value.Z;
                #else
                Data.PositionPhi.x = value.X;
                Data.PositionPhi.y = value.Y;
                Data.PositionPhi.z = value.Z;
                #endif
                cachedPosition = new Vector3(value.X, value.Y, value.Z);
                cachedEmplacement = value;
            }
        }

        public TrileInstanceData Data = new TrileInstanceData();

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
            #if !UNITY
            Data.PositionPhi.W = (float) ((int) orientation - 2) * 1.570796f;
            #else
            Data.PositionPhi.w = (float) ((int) orientation - 2) * 1.570796f;
            #endif
            
            phiQuat = TrileInstance.QuatLookup[orientation];
            phiOri = TrileInstance.OrientationLookup[orientation];
        }
    	
    }
}
#endif

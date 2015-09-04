#if !FEZENGINE
using System.Collections;
using System.Collections.Generic;
using FezEngine;
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
    public class ArtObjectActorSettings {

        public bool Inactive;
        public ActorType ContainedTrile;
        public int? AttachedGroup;
        public float SpinOffset;
        public float SpinEvery;
        public Viewpoint SpinView;
        public bool OffCenter;
        public Vector3 RotationCenter;
        public VibrationMotor[] VibrationPattern;
        public CodeInput[] CodePattern;
        public PathSegment Segment;
        public int? NextNode;
        public string DestinationLevel;
        public string TreasureMapName;
        public float TimeswitchWindBackSpeed;
        public List<FaceOrientation> InvisibleSides; //originally HashSet
        public ArtObjectInstance NextNodeAo;
        public ArtObjectInstance PrecedingNodeAo;
        public bool ShouldMoveToEnd;
        public float? ShouldMoveToHeight;

    }
}
#endif

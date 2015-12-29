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
    [System.Serializable]
    public class Trile {

        public int Id;
    	public string Name;
        public string CubemapPath;
        public Dictionary<FaceOrientation, CollisionType> Faces;
        public ShaderInstancedIndexedPrimitives<VertexPositionNormalTextureInstance, Vector4> Geometry;
        public TrixelCluster MissingTrixels;
        public TrileActorSettings ActorSettings;
        public bool Immaterial;
        public bool SeeThrough;
        public bool Thin;
        public bool ForceHugging;
        public TrileSet TrileSet;
    	public Vector3 Size;
    	public Vector3 Offset;
        public SurfaceType SurfaceType;
    	public Vector3 AtlasOffset;
        public bool ForceKeep;

        public Trile() {
            MissingTrixels = new TrixelCluster();
            ActorSettings = new TrileActorSettings();
            Name = "Untitled";
            #if !UNITY
            Size = Vector3.One;
            #else
            Size = Vector3.one;
            #endif
            Faces = new Dictionary<FaceOrientation, CollisionType>(4);
        }
    	
    }
}
#endif

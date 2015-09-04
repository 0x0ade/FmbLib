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
    public class ArtObject {
        
        public string Name;
        public string CubemapPath;
        public Texture2D Cubemap;
        public Vector3 Size;
        public ActorType ActorType;
        public bool NoSihouette;
        public TrixelCluster MissingTrixels;
        //public List<TrixelSurface> TrixelSurfaces; //TODO
        public ShaderInstancedIndexedPrimitives<VertexPositionNormalTextureInstance, Matrix> Geometry;
        
    }
}
#endif

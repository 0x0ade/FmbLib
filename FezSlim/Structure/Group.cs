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
    public class Group {

        public int Id;
        public Mesh Mesh;
        public bool RotateOffCenter;
        public bool Enabled;
        public Material Material;
        //public Dirtyable<Matrix?> TextureMatrix; //TODO
        //public CullMode? CullMode; //TODO
        public bool? AlwaysOnTop;
        //public BlendingMode? Blending; //TODO
        //public SamplerState SamplerState; //TODO
        public bool? NoAlphaWrite;
        //public TexturingType TexturingType; //TODO
        public Texture Texture;
        public object CustomData;
        //public IIndexedPrimitiveCollection Geometry; //TODO
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;
        //public Dirtyable<Matrix> WorldMatrix; //TODO
        //public Dirtyable<Matrix> InverseTransposeWorldMatrix; //TODO
        
        public Group(Mesh mesh, int id) { //usually internal but who cares
            Id = id;
            Mesh = mesh;
            Enabled = true;
            //TextureMatrix.Dirty = true; //TODO
            //TranslationMatrix = ScalingRotationMatrix = ScalingMatrix = RotationMatrix = WorldMatrix = Matrix.Identity; //TODO
        }
    	
    }
}
#endif

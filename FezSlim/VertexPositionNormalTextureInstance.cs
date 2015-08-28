#if !FEZENGINE
using System;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#elif UNITY
using UnityEngine;
#else
#warning FmbLib slim XNA still WIP.
#endif

namespace FezEngine.Structure.Geometry {
    public class VertexPositionNormalTextureInstance {

        public static readonly Vector3[] ByteToNormal = new Vector3[6] {
            #if !UNITY
            Vector3.Left,
            Vector3.Down,
            Vector3.Forward,
            Vector3.Right,
            Vector3.Up,
            Vector3.Backward
            #else
            Vector3.left,
            Vector3.down,
            Vector3.forward,
            Vector3.right,
            Vector3.up,
            Vector3.back
            #endif
        };

        public Vector3 Position { get; set; }
        public Vector3 Normal { get; set; }
        public Vector2 TextureCoordinate { get; set; }
        public float InstanceIndex { get; set; }
        
        public VertexPositionNormalTextureInstance(Vector3 position, Vector3 normal)
            : this(position, normal, -1f) {
        }
        
        public VertexPositionNormalTextureInstance(Vector3 position, byte normal, Vector2 textureCoordinate) {
            Position = position;
            Normal = VertexPositionNormalTextureInstance.ByteToNormal[(int) normal];
            TextureCoordinate = textureCoordinate;
            InstanceIndex = -1f;
        }
        
        public VertexPositionNormalTextureInstance(Vector3 position, Vector3 normal, float instanceIndex) {
            Position = position;
            Normal = normal;
            InstanceIndex = instanceIndex;
        }
    }
}
#endif
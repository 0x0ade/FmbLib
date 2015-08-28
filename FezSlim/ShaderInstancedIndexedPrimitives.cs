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
    public class ShaderInstancedIndexedPrimitives<TemplateType, InstanceType> {

        public int InstancesPerBatch; //usually private readonly, but who cares

        public bool NeedsEffectCommit { get; set; }

        public TemplateType[] Vertices { get; set; }
        
        public int[] Indices { get; set; }
        
        public ShaderInstancedIndexedPrimitives(PrimitiveType type, int instancesPerBatch) {
            InstancesPerBatch = instancesPerBatch;
        }

    }
}
#endif

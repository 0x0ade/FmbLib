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
    public class TrileSet {

    	public string Name;
        public Dictionary<int, Trile> Triles;
        public Texture2D TextureAtlas;

        public TrileSet() {
            Triles = new Dictionary<int, Trile>();
        }
        
        public void OnDeserialization() {
            foreach (int index in Triles.Keys) {
                Triles[index].TrileSet = this;
                Triles[index].Id = index;
            }
        }


    }
}
#endif

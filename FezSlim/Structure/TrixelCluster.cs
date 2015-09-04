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
    public class TrixelCluster {

        public static readonly Vector3[] Directions = new Vector3[6] { //Usually private, but used by FindBiggestBox and VisitChunk, which don't exist here
            #if !UNITY
            Vector3.Up,
            Vector3.Down,
            Vector3.Left,
            Vector3.Right,
            Vector3.Forward,
            Vector3.Backward
            #else
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
            #endif
        };

        public List<TrixelCluster.Box> Boxes;
        public List<TrixelEmplacement> Orphans;
        
        //public List<TrixelCluster.Chunk> Chunks; //Will be filled on OnDeserialization, but direct call to Boxes and Orphans should be okay
        
        public TrixelCluster() {
            //Chunks = new List<TrixelCluster.Chunk>();
        }
        
        public void OnDeserialization() {
            //Fills Chunks, which replaces Boxes and Orphans and then sets the "deserialized" forms to null.
            /*
            if (Orphans != null) {
                using (List<TrixelEmplacement>.Enumerator enumerator = Orphans.GetEnumerator()) {
                    while (enumerator.MoveNext()) {
                        TrixelEmplacement trixel = enumerator.Current;
                        TrixelCluster.Chunk chunk = Enumerable.FirstOrDefault<TrixelCluster.Chunk>((IEnumerable<TrixelCluster.Chunk>) this.Chunks, (Func<TrixelCluster.Chunk, bool>) (c => c.IsNeighbor(trixel)));
                        if (chunk == null) {
                            Chunks.Add(chunk = new TrixelCluster.Chunk());
                        }
                        chunk.Trixels.Add(trixel);
                    }
                }
                //Orphans = null;
            }
            if (Boxes != null) {
                using (List<TrixelCluster.Box>.Enumerator enumerator = Boxes.GetEnumerator()) {
                    while (enumerator.MoveNext()) {
                        TrixelCluster.Box box = enumerator.Current;
                        TrixelCluster.Chunk chunk = Enumerable.FirstOrDefault<TrixelCluster.Chunk>((IEnumerable<TrixelCluster.Chunk>) this.Chunks, (Func<TrixelCluster.Chunk, bool>) (c => c.IsNeighbor(box)));
                        if (chunk == null) {
                            Chunks.Add(chunk = new TrixelCluster.Chunk());
                        }
                        chunk.Boxes.Add(box);
                    }
                }
                //Boxes = null;
            }
            */
        }

        public class Box {
            public TrixelEmplacement Start;
            public TrixelEmplacement End;
        }
    	
    }
}
#endif

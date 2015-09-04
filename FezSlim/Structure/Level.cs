#if !FEZENGINE
using System.Collections;
using System.Collections.Generic;
using FezEngine.Structure.Scripting;

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
    public class Level {
        
        public Dictionary<TrileEmplacement, TrileInstance> Triles;
        public bool Flat;
        public string Name;
        public TrileFace StartingPosition;
        public Vector3 Size;
        public string SequenceSamplesPath;
        public bool SkipPostProcess;
        public float BaseDiffuse;
        public float BaseAmbient;
        public string GomezHaloName;
        public bool HaloFiltering;
        public bool BlinkingAlpha;
        public bool Loops;
        public LiquidType WaterType;
        public float WaterHeight;
        public bool Descending;
        public bool Rainy;
        public bool LowPass;
        public LevelNodeType NodeType;
        public int FAPFadeOutStart;
        public int FAPFadeOutLength;
        public bool Quantum;
        public string SkyName;
        public string TrileSetName;
        public string SongName;
        public Dictionary<int, Volume> Volumes;
        public Dictionary<int, ArtObjectInstance> ArtObjects;
        public Dictionary<int, BackgroundPlane> BackgroundPlanes;
        public Dictionary<int, Script> Scripts;
        public Dictionary<int, TrileGroup> Groups;
        public Dictionary<int, NpcInstance> NonPlayerCharacters;
        public Dictionary<int, MovementPath> Paths;
        public List<string> MutedLoops;
        public List<AmbienceTrack> AmbienceTracks;

        //Loaded after deserializing, currently can be ignored
        //public Sky Sky; //TODO
        public TrileSet TrileSet;
        //public TrackedSong Song; //TODO
        
        public Level() {
            Triles = new Dictionary<TrileEmplacement, TrileInstance>();
            Volumes = new Dictionary<int, Volume>();
            ArtObjects = new Dictionary<int, ArtObjectInstance>();
            BackgroundPlanes = new Dictionary<int, BackgroundPlane>();
            Groups = new Dictionary<int, TrileGroup>();
            Scripts = new Dictionary<int, Script>();
            NonPlayerCharacters = new Dictionary<int, NpcInstance>();
            Paths = new Dictionary<int, MovementPath>();
            MutedLoops = new List<string>();
            AmbienceTracks = new List<AmbienceTrack>();
            BaseDiffuse = 1f;
            BaseAmbient = 0.35f;
            HaloFiltering = true;
        }
        
        public void OnDeserialization() {
            foreach (TrileEmplacement key in Triles.Keys) {
                TrileInstance trile = Triles[key];
                /*if (Triles[key].Emplacement != key) {
                    Triles[key].Emplacement = key;
                }*/
                //trile.Update();
                trile.OriginalEmplacement = key;
                if (trile.OverlappedTriles.Count > 0) {
                    foreach (TrileInstance trileOverlapping in trile.OverlappedTriles) {
                        trileOverlapping.OriginalEmplacement = key;
                    }
                }
            }
            foreach (int id in Scripts.Keys) {
                Scripts[id].Id = id;
            }
            foreach (int id in Volumes.Keys) {
                Volumes[id].Id = id;
            }
            foreach (int id in NonPlayerCharacters.Keys) {
                NonPlayerCharacters[id].Id = id;
            }
            foreach (int id in ArtObjects.Keys) {
                ArtObjects[id].Id = id;
            }
            foreach (int id in BackgroundPlanes.Keys) {
                BackgroundPlanes[id].Id = id;
            }
            foreach (int id in Paths.Keys) {
                Paths[id].Id = id;
            }
            foreach (int id in Groups.Keys)  {
                TrileGroup trileGroup = Groups[id];
                trileGroup.Id = id;
                TrileEmplacement[] trileEmplacementArray = new TrileEmplacement[trileGroup.Triles.Count];
                for (int i = 0; i < trileEmplacementArray.Length; i++) {
                    trileEmplacementArray[i] = trileGroup.Triles[i].Emplacement;
                }
                trileGroup.Triles.Clear();
                foreach (TrileEmplacement key in trileEmplacementArray) {
                    trileGroup.Triles.Add(Triles[key]);
                }
            }
        }
        
    }
}
#endif

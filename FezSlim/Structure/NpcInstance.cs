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
    public class NpcInstance {

        public readonly NpcMetadata Metadata = new NpcMetadata();
        public int Id;
        public bool Talking;
        public bool Enabled;
        public bool Visible;
        public string Name;
        public Vector3 Position;
        public Vector3 DestinationOffset;
        public bool RandomizeSpeech;
        public bool SayFirstSpeechLineOnce;
        public List<SpeechLine> Speech;
        public SpeechLine CustomSpeechLine;
        public Group Group;
        //public NpcState State; //TODO (that's the game component of the NPC - implement it?)
        public Dictionary<NpcAction, NpcActionContent> Actions;

        public float WalkSpeed {
            get {
                return Metadata.WalkSpeed;
            }
            set {
                Metadata.WalkSpeed = value;
            }
        }
        public bool AvoidsGomez {
            get {
                return Metadata.AvoidsGomez;
            }
            set {
                Metadata.AvoidsGomez = value;
            }
        }
        public ActorType ActorType {
            get {
                return Metadata.ActorType;
            }
            set {
                Metadata.ActorType = value;
            }
        }

        public NpcInstance() {
            Speech = new List<SpeechLine>();
            Actions = new Dictionary<NpcAction, NpcActionContent>();
            Enabled = true;
            Visible = true;
        }

        public void FillMetadata(NpcMetadata md) {
            Metadata.AvoidsGomez = md.AvoidsGomez;
            Metadata.WalkSpeed = md.WalkSpeed;
            Metadata.SoundPath = md.SoundPath;
            Metadata.SoundActions = md.SoundActions;
        }

    }
}
#endif

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
    public class InstanceActorSettings {

        public const int Steps = 16;

        public bool Inactive;
        public int? ContainedTrile;
        public string SignText;
        public bool[] Sequence;
        public string SequenceSampleName;
        public string SequenceAlternateSampleName;
        public int? HostVolume;

        public InstanceActorSettings() {
        }

    }
}
#endif

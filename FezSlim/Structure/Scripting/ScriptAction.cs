#if !FEZENGINE
using System;
using System.Collections;
using System.Collections.Generic;
using FezEngine.Structure;
using FezEngine.Structure.Geometry;
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

namespace FezEngine.Structure.Scripting {
    public class ScriptAction : ScriptPart {

        public string Operation;
        public bool Killswitch;
        public bool Blocking;
        public string[] Arguments;
        public object[] ProcessedArguments; //Is that required? (private setter)
        //public DynamicMethodDelegate Invoke; //TODO (private setter)

        public void OnDeserialization() {
            //STUB
        }
    	
    }
}
#endif

﻿#if !FEZENGINE
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
    public class ScriptCondition : ScriptPart {

        public string Property;
        public ComparisonOperator Operator;
        public string Value;
        
        public ScriptCondition() {
            Operator = ComparisonOperator.None;
        }

        public void OnDeserialization() {
            //would call Process, but stub here
        }
    	
    }
}
#endif

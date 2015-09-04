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
    public class Script {

        public int Id;
    	public string Name;
        public bool OneTime;
        public bool LevelWideOneTime;
        public bool Disabled;
        public bool Triggerless;
        public bool IgnoreEndTriggers;
        public bool IsWinCondition;
        public TimeSpan? Timeout;
        public List<ScriptTrigger> Triggers;
        public List<ScriptAction> Actions;
        public List<ScriptCondition> Conditions;
        public bool ScheduleEvalulation;

        public Script() {
            Name = "Untitled";
            Triggers = new List<ScriptTrigger>();
            Actions = new List<ScriptAction>();
        }
    	
    }
}
#endif

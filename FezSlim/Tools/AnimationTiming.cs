#if !FEZENGINE
using System;
using System.Collections;
using System.Collections.Generic;
using FezEngine.Structure.Geometry;
using FmbLib;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#elif UNITY
using UnityEngine;
#else
#warning FmbLib slim XNA still WIP.
#endif

namespace FezEngine.Tools {
    public class AnimationTiming {
        public readonly float[] FrameTimings;
        public readonly int InitialFirstFrame;
        public readonly int InitialEndFrame;
        private readonly float stepPerFrame;
        private float startStep;
        private float endStep;
        private int startFrame;
        private int endFrame;
    
        public bool Loop;
    
        public bool Paused;
    
        public float Step;
    
        public float NormalizedStep {
            get {
                return (Step - startStep) / endStep;
            }
        }
    
        public int StartFrame {
            get {
                return startFrame;
            }
            set {
                startFrame = value;
                startStep = startFrame / (float) FrameTimings.Length;
            }
        }
    
        public int EndFrame {
            get {
                return endFrame;
            }
            set {
                endFrame = value;
                endStep = (endFrame + 1f) / FrameTimings.Length;
            }
        }
    
        public float StartStep {
            get {
                return startStep;
            }
        }
    
        public float EndStep {
            get {
                return endStep;
            }
        }
    
        public bool Ended {
            get {
                return !Loop && Math.Abs(Step - endStep) < 0.05D; //AlmostEqual
            }
        }
    
        public int Frame {
            get {
                return (int) Math.Floor(Step * (double) FrameTimings.Length);
            }
            set {
                Step = value / (float) FrameTimings.Length;
            }
        }
    
        public float NextFrameContribution {
            get
            {
                return Step * FrameTimings.Length % 1f; //Frac
            }
        }
    
        public AnimationTiming(int startFrame, int endFrame, float[] frameTimings)
        : this(startFrame, endFrame, false, frameTimings) {
        }
    
        public AnimationTiming(int startFrame, int endFrame, bool loop, float[] frameTimings) {
            Loop = loop;
            FrameTimings = FmbHelper.Select<float>(frameTimings, delegate(float x) { return x != 0f ? x : 0.1f; });
            stepPerFrame = 1f / (float) frameTimings.Length;
            InitialFirstFrame = StartFrame = startFrame;
            InitialEndFrame = EndFrame = endFrame;
        }
    
        public void Restart() {
            Step = startStep;
            Paused = false;
        }
    
        public void Update(TimeSpan elapsed) {
            Update(elapsed, 1f);
        }
    
        public void Update(TimeSpan elapsed, float timeFactor) {
            if (Paused || Ended) {
                return;
            }
            int index = (int) Math.Floor(Step * (double) FrameTimings.Length);
            Step += (float) elapsed.TotalSeconds * timeFactor / FrameTimings[index] * stepPerFrame;
            while (Step >= endStep) {
                if (Loop) {
                    Step -= endStep - startStep;
                } else {
                    Step = endStep - 1f / 1000f;
                }
            }
            while (Step < startStep) {
                if (Loop) {
                    Step += (float) ((double) endStep - (double) startStep - 1D / 1000D);
                } else {
                    Step = startStep;
                }
            }
        }
    
        public void RandomizeStep() {
            //TODO
            //Step = RandomHelper.Between((double) startStep, (double) endStep);
        }
    
        public AnimationTiming Clone() {
            return new AnimationTiming(StartFrame, EndFrame, Loop, FrameTimings);
        }
    }
}
#endif

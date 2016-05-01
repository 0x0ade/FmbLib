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
                startStep = (float) startFrame / (float) FrameTimings.Length;
            }
        }
    
        public int EndFrame {
            get {
                return endFrame;
            }
            set {
                endFrame = value;
                endStep = (float) (endFrame + 1) / (float) FrameTimings.Length;
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
                return !Loop && FezMath.AlmostEqual(Step, endStep);
            }
        }
    
        public int Frame {
            get {
                return (int) Math.Floor((double) Step * (double) FrameTimings.Length);
            }
            set {
                Step = (float) value / (float) FrameTimings.Length;
            }
        }
    
        public float NextFrameContribution {
            get
            {
                return FezMath.Frac(Step * (float) FrameTimings.Length);
            }
        }
    
        public AnimationTiming(int startFrame, int endFrame, float[] frameTimings)
        : this(startFrame, endFrame, false, frameTimings)
        {
        }
    
        public AnimationTiming(int startFrame, int endFrame, bool loop, float[] frameTimings)
        {
        Loop = loop;
        FrameTimings = Enumerable.ToArray<float>(Enumerable.Select<float, float>((IEnumerable<float>) frameTimings, (Func<float, float>) (x =>
        {
            if ((double) x != 0.0)
            return x;
            else
            return 0.1f;
        })));
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
    
        public void Update(TimeSpan elapsed, float timeFactor)
        {
        if (Paused || Ended)
            return;
        int index = (int) Math.Floor((double) Step * (double) FrameTimings.Length);
        Step += (float) elapsed.TotalSeconds * timeFactor / FrameTimings[index] * stepPerFrame;
        while ((double) Step >= (double) endStep)
        {
            if (Loop)
            Step -= endStep - startStep;
            else
            Step = endStep - 1.0 / 1000.0;
        }
        while ((double) Step < (double) startStep)
        {
            if (Loop)
            Step += (float) ((double) endStep - (double) startStep - 1.0 / 1000.0);
            else
            Step = startStep;
        }
        }
    
        public void RandomizeStep() {
        Step = RandomHelper.Between((double) startStep, (double) endStep);
        }
    
        public AnimationTiming Clone() {
        return new AnimationTiming(StartFrame, EndFrame, Loop, FrameTimings);
        }
    }
}
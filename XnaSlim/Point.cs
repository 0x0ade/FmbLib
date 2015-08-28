#if !XNA
using System;

#if !UNITY
namespace Microsoft.Xna.Framework {
#else
namespace UnityEngine {
#endif
    public class Point {
        public int X;
        public int Y;
        
        public Point(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
#endif

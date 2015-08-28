#if !XNA
using System;

#if !UNITY
namespace Microsoft.Xna.Framework {
#else
namespace UnityEngine {
#endif
    public class Rectangle {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public Rectangle(int x, int y, int width, int height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
#endif

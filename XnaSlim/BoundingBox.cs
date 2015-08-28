#if !XNA
using System;

#if !UNITY
namespace Microsoft.Xna.Framework {
#else
namespace UnityEngine {
#endif
    public class BoundingBox {
        
        public Vector3 Min;
        public Vector3 Max;
        
        public BoundingBox(Vector3 min, Vector3 max) {
            Min = min;
            Max = max;
        }
    }
}
#endif

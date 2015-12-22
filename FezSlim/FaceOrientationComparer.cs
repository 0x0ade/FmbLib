#if !FEZENGINE
namespace FezEngine {
    public class FaceOrientationComparer {
        public static readonly FaceOrientationComparer Default = new FaceOrientationComparer();

        public bool Equals(FaceOrientation x, FaceOrientation y) {
            return x == y;
        }

        public int GetHashCode(FaceOrientation obj) {
            return (int) obj;
        }
    }
}
#endif

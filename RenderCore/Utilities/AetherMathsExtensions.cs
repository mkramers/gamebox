using System.Numerics;

namespace RenderCore.Utilities
{
    public static class AetherMathsExtensions
    {
        public static Vector2 GetVector2(this Aether.Physics2D.Common.Maths.Vector2 _vector)
        {
            return new Vector2(_vector.X, _vector.Y);
        }

        public static Aether.Physics2D.Common.Maths.Vector2 GetVector2(this Vector2 _vector)
        {
            return new Aether.Physics2D.Common.Maths.Vector2(_vector.X, _vector.Y);
        }
    }
}
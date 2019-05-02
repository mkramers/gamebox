using System.Numerics;

namespace RenderCore
{
    public static class AetherMathsConverter
    {
        public static Vector2 GetVector2(this Aether.Physics2D.Common.Maths.Vector2 _vector)
        {
            return new Vector2(_vector.X, _vector.Y);
        }
    }
}
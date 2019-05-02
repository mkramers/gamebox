using System.Numerics;
using SFML.System;

namespace RenderCore
{
    public static class AetherMathsConverter
    {
        public static Vector2 GetVector2(this Aether.Physics2D.Common.Maths.Vector2 _vector)
        {
            return new Vector2(_vector.X, _vector.Y);
        }

        public static Vector2f GetVector2F(this Aether.Physics2D.Common.Maths.Vector2 _vector)
        {
            return new Vector2f(_vector.X, _vector.Y);
        }
    }
}
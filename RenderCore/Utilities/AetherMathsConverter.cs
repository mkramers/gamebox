using System.Numerics;
using SFML.System;

namespace RenderCore.Utilities
{
    public static class AetherMathsConverter
    {
        public static Vector2 GetVector2(this Aether.Physics2D.Common.Maths.Vector2 _vector)
        {
            return new Vector2(_vector.X, _vector.Y);
        }
    }

    public static class SFMLConverterExtensions
    {
        public static Vector2 GetVector2(this Vector2u _vector)
        {
            return new Vector2(_vector.X, _vector.Y);
        }
    }
}
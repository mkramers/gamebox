using System.Numerics;
using SFML.System;
using AetherMath = Aether.Physics2D.Common.Maths;

namespace RenderCore.Utilities
{
    public static class Vector2Converter
    {
        public static Vector2f GetVector2F(this Vector2 _vector)
        {
            return new Vector2f(_vector.X, _vector.Y);
        }

        public static AetherMath.Vector2 GetVector2(this Vector2 _vector)
        {
            return new AetherMath.Vector2(_vector.X, _vector.Y);
        }
    }
}
using System.Numerics;
using SFML.System;

namespace RenderCore.Utilities
{
    public static class Vector2FConverter
    {
        public static Vector2 GetVector2(this Vector2f _vector)
        {
            return new Vector2(_vector.X, _vector.Y);
        }
    }
}
using System.Numerics;
using SFML.System;

namespace RenderCore
{
    public static class Vector2Converter
    {
        public static Vector2f GetVector2f(this Vector2 _vector)
        {
            return new Vector2f(_vector.X, _vector.Y);
        }
    }
}
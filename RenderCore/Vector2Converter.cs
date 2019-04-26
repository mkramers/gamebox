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
    public static class Vector2fConverter
    {
        public static Vector2 GetVector2(this Vector2f _vector)
        {
            return new Vector2(_vector.X, _vector.Y);
        }
    }
}
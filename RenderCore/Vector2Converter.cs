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

    public static class Vector3Converter
    {
        public static Vector2f GetVector2f(this Vector3 _vector)
        {
            return new Vector2f(_vector.X, _vector.Y);
        }

        public static Vector3f GetVector3f(this Vector3 _vector)
        {
            return new Vector3f(_vector.X, _vector.Y, _vector.Z);
        }
    }
}
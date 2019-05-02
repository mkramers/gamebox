using System.Numerics;
using SFML.System;

namespace RenderCore
{
    public static class Vector3Converter
    {
        public static Vector2f GetVector2F(this Vector3 _vector)
        {
            return new Vector2f(_vector.X, _vector.Y);
        }

        public static Vector3f GetVector3F(this Vector3 _vector)
        {
            return new Vector3f(_vector.X, _vector.Y, _vector.Z);
        }
    }
}
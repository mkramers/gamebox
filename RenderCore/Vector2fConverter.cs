using System.Numerics;
using SFML.System;

namespace RenderCore
{
    public static class Vector2fConverter
    {
        public static Vector2 GetVector2(this Vector2f _vector)
        {
            return new Vector2(_vector.X, _vector.Y);
        }
    }
}
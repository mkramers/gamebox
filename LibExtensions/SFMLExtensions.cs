using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace LibExtensions
{
    // ReSharper disable once InconsistentNaming
    public static class SFMLExtensions
    {
        public static Vector2f GetVector2F(this Vector2 _vector)
        {
            return new Vector2f(_vector.X, _vector.Y);
        }
        public static Vector2f GetVector2F(this Vector2u _vector)
        {
            return new Vector2f(_vector.X, _vector.Y);
        }

        public static Vector2 GetVector2(this Vector2f _vector)
        {
            return new Vector2(_vector.X, _vector.Y);
        }

        public static Vector2 GetSize(this FloatRect _rect)
        {
            Vector2 size = new Vector2(_rect.Width, _rect.Height);
            return size;
        }
    }
}
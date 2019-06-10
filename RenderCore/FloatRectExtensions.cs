using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class FloatRectExtensions
    {
        public static Vector2 GetSize(this FloatRect _rect)
        {
            Vector2 size = new Vector2(_rect.Width, _rect.Height);
            return size;
        }
    }
}
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public interface IDrawable : Drawable
    {
        void SetRenderPosition(Vector2 _position);
    }
}
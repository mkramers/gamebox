using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public interface IDrawable : Drawable, IDisposable
    {
        void SetRenderPosition(Vector2 _position);
    }
}
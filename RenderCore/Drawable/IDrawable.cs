using System;
using Common.Geometry;

namespace RenderCore.Drawable
{
    public interface IDrawable : SFML.Graphics.Drawable, IDisposable, IPosition
    {
    }
}
using System;
using RenderCore.Widget;
using SFML.Graphics;

namespace RenderCore.Drawable
{
    public abstract class TickableDrawable<T> : Drawable<T>, IWidget where T : Transformable, SFML.Graphics.Drawable
    {
        protected TickableDrawable(T _renderObject) : base(_renderObject)
        {
        }

        public abstract void Tick(TimeSpan _elapsed);
    }
}
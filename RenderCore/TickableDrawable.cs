using System;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class TickableDrawable<T> : Drawable<T>, IWidget where T : Transformable, Drawable
    {
        protected TickableDrawable(T _renderObject) : base(_renderObject)
        {
        }

        public abstract void Tick(TimeSpan _elapsed);
    }
}
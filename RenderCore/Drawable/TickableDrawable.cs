using System;
using Common.Tickable;
using SFML.Graphics;

namespace RenderCore.Drawable
{
    public abstract class TickableDrawable<T> : Drawable<T>, ITickable where T : Transformable, SFML.Graphics.Drawable
    {
        protected TickableDrawable(T _renderObject) : base(_renderObject)
        {
        }

        public abstract void Tick(TimeSpan _elapsed);
    }
}
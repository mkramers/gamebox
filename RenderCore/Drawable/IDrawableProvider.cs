using System.Collections.Generic;

namespace RenderCore.Drawable
{
    public interface IDrawableProvider
    {
        IEnumerable<IDrawable> GetDrawables();
    }
}
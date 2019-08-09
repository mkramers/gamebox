using System.Collections.Generic;
using RenderCore.Drawable;

namespace RenderBox.New
{
    public interface IDrawableProvider
    {
        IEnumerable<IDrawable> GetDrawables();
    }
}
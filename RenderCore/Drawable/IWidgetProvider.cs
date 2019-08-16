using System.Collections.Generic;
using RenderCore.Widget;

namespace RenderCore.Drawable
{
    public interface IWidgetProvider
    {
        IEnumerable<IGuiWidget> GetWidgets();
    }
}
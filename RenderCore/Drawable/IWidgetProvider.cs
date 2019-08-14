using System.Collections.Generic;

namespace RenderCore.Drawable
{
    public interface IWidgetProvider
    {
        IEnumerable<TGUI.Widget> GetWidgets();
    }
}
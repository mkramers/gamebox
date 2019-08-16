using System.Collections.Generic;
using RenderCore.Widget;

namespace RenderCore.Drawable
{
    public class WidgetProvider : IWidgetProvider
    {
        private readonly IGuiWidget m_widget;

        public WidgetProvider(IGuiWidget _widget)
        {
            m_widget = _widget;
        }

        public IEnumerable<IGuiWidget> GetWidgets()
        {
            return new[] {m_widget};
        }
    }
}
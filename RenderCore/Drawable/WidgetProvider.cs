using System.Collections.Generic;

namespace RenderCore.Drawable
{
    public class WidgetProvider : IWidgetProvider
    {
        private readonly TGUI.Widget m_widget;

        public WidgetProvider(TGUI.Widget _widget)
        {
            m_widget = _widget;
        }

        public IEnumerable<TGUI.Widget> GetWidgets()
        {
            return new[] { m_widget };
        }
    }
}
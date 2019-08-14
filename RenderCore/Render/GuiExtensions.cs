using System.Collections.Generic;
using System.Linq;
using TGUI;

namespace RenderCore.Render
{
    public static class GuiExtensions
    {
        public static void UpdateCurrentWidgets(this Gui _gui, IEnumerable<TGUI.Widget> _widgets)
        {
            List<TGUI.Widget> currentWidgets = _gui.GetWidgets();

            IEnumerable<TGUI.Widget> widgets = _widgets as TGUI.Widget[] ?? _widgets.ToArray();

            foreach (TGUI.Widget widget in widgets)
            {
                if (!currentWidgets.Contains(widget))
                {
                    _gui.Add(widget);
                }
            }

            foreach (TGUI.Widget currentWidget in currentWidgets)
            {
                if (!widgets.Contains(currentWidget))
                {
                    _gui.Remove(currentWidget);
                }
            }
        }
    }
}
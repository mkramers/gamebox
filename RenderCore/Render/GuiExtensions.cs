using System.Collections.Generic;
using System.Linq;
using RenderCore.Widget;
using TGUI;

namespace RenderCore.Render
{
    public static class GuiExtensions
    {
        public static void UpdateCurrentWidgets(this Gui _gui, IEnumerable<IGuiWidget> _widgets)
        {
            List<TGUI.Widget> currentWidgets = _gui.GetWidgets();

            IEnumerable<IGuiWidget> allWidgets = _widgets as IGuiWidget[] ?? _widgets.ToArray();

            foreach (IGuiWidget guiWidget in allWidgets)
            {
                TGUI.Widget widget = guiWidget.GetWidget();
                if (!currentWidgets.Contains(widget))
                {
                    _gui.Add(widget);
                }
            }

            TGUI.Widget[] widgets = allWidgets.Select(_widget => _widget.GetWidget()).ToArray();
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
using System.Collections.Generic;
using System.Linq;
using RenderCore.Widget;

namespace RenderCore.Render
{
    public static class GuiExtensions
    {
        public static void UpdateCurrentWidgets(this IGui _gui, IEnumerable<IGuiWidget> _widgets)
        {
            IGuiWidget[] currentWidgets = _gui.GetWidgets().ToArray();

            IEnumerable<IGuiWidget> allWidgets = _widgets as IGuiWidget[] ?? _widgets.ToArray();

            foreach (IGuiWidget widget in allWidgets)
            {
                if (!currentWidgets.Contains(widget))
                {
                    _gui.Add(widget);
                }
            }

            foreach (IGuiWidget currentWidget in currentWidgets)
            {
                if (!allWidgets.Contains(currentWidget))
                {
                    _gui.Remove(currentWidget);
                }
            }
        }
    }
}
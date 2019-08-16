using System.Numerics;
using TGUI;

namespace LibExtensions.TGUI
{
    public static class WidgetExtensions
    {
        public static void SetPosition(this Widget _widget, Vector2 _position)
        {
            _widget.Position = _position.GetVector2F();
        }
    }
}
using System;
using SFML.Graphics;

namespace RenderCore.Widget
{
    public interface IGuiWidget : IDisposable
    {
        void OnViewChanged(View _view);
        TGUI.Widget GetWidget();
    }
}
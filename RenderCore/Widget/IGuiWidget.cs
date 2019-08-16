using SFML.Graphics;

namespace RenderCore.Widget
{
    public interface IGuiWidget
    {
        void OnViewChanged(View _view);
        TGUI.Widget GetWidget();
    }
}
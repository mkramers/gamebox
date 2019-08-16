using SFML.Graphics;

namespace RenderCore.Render
{
    public static class RenderWindowWrapperExtensions
    {
        public static void SetViewport(this IRenderWindow _renderWindow, FloatRect _viewPort)
        {
            View renderWindowView = _renderWindow.GetView();
            renderWindowView.Viewport = _viewPort;
        }
    }
}
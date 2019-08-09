using RenderCore.ViewProvider;
using SFML.Graphics;

namespace RenderBox.New
{
    public static class RenderWindowExtensions
    {
        public static void SetView(this RenderWindow _renderWindow, IViewProvider _viewProvider)
        {
            _renderWindow.SetView(_viewProvider.GetView());
        }
    }
}
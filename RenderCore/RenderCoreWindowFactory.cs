using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public static class RenderCoreWindowFactory
    {
        public static RenderCoreWindow CreateRenderCoreWindow(string _windowTitle, Vector2u _windowSize,
            FloatRect _viewRect)
        {
            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(_windowTitle, _windowSize, _viewRect);

            GridWidget gridWidget = new GridWidget(renderWindow.GetView()) {IsDrawEnabled = true};

            return new RenderCoreWindow(renderWindow, new[] {gridWidget});
        }
    }
}
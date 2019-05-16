using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public static class RenderCoreWindowFactory
    {
        public static RenderCoreWindow CreateRenderCoreWindow(string _windowTitle, Vector2u _windowSize,
            FloatRect _viewRect)
        {
            IViewProvider viewProvider = new TickableView(0.0625f);

            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(_windowTitle, _windowSize, _viewRect);

            return new RenderCoreWindow(renderWindow, viewProvider);
        }
    }
}
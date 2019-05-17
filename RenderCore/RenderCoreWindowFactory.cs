using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public static class RenderCoreWindowFactory
    {
        public static RenderCoreWindow CreateRenderCoreWindow(string _windowTitle, Vector2u _windowSize)
        {
            View view = new View(new Vector2f(0, 0), new Vector2f(50, 50))
            {
                Center = new Vector2f(0, 0),
            };

            IViewProvider viewProvider = new TickableView(view, 0.0625f);
            
            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(_windowTitle, _windowSize);

            return new RenderCoreWindow(renderWindow, viewProvider);
        }
    }
}
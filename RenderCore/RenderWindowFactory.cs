using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore
{
    public static class RenderWindowFactory
    {
        private static RenderWindow CreateRenderWindow(string _name, Vector2u _windowSize, FloatRect _viewRect,
            FloatRect _viewPortRect)
        {
            Vector2f center = new Vector2f(_viewRect.Left + _viewRect.Width / 2, _viewRect.Top - _viewRect.Height / 2);

            View view = new View(_viewRect) {Viewport = _viewPortRect, Center = center};

            VideoMode videoMode = new VideoMode(_windowSize.X, _windowSize.Y);
            RenderWindow window = new RenderWindow(videoMode, _name);
            window.SetView(view);

            return window;
        }

        public static RenderWindow CreateRenderWindow(string _name, Vector2u _windowSize, FloatRect _viewRect)
        {
            FloatRect viewPortRect = new FloatRect(0, 0, 1.0f, 1.0f);

            return CreateRenderWindow(_name, _windowSize, _viewRect, viewPortRect);
        }
    }
}
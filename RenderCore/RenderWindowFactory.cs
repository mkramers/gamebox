using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore
{
    public class RenderWindowFactory
    {
        public static RenderWindow CreateRenderWindow(string _name, Vector2u _windowSize, FloatRect _viewRect,
            FloatRect _viewPortRect)
        {
            View view = new View(_viewRect) {Viewport = _viewPortRect};

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
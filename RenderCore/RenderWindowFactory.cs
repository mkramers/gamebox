using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore
{
    public static class RenderWindowFactory
    {
        public static RenderWindow CreateRenderWindow(string _name, Vector2u _windowSize)
        {
            VideoMode videoMode = new VideoMode(_windowSize.X, _windowSize.Y);
            RenderWindow window = new RenderWindow(videoMode, _name);

            return window;
        }
    }
}
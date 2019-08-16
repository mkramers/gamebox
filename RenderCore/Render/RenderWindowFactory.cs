using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore.Render
{
    public static class RenderWindowFactory
    {
        public static SFML.Graphics.RenderWindow CreateRenderWindow(string _name, Vector2u _windowSize)
        {
            VideoMode videoMode = new VideoMode(_windowSize.X, _windowSize.Y);
            SFML.Graphics.RenderWindow window = new SFML.Graphics.RenderWindow(videoMode, _name);

            return window;
        }
    }
}
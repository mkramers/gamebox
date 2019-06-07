using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public static class RenderCoreWindowFactory
    {
        public static RenderCoreWindow CreateRenderCoreWindow(string _windowTitle, Vector2u _windowSize, float _aspectRatio)
        {
            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(_windowTitle, _windowSize);

            return new RenderCoreWindow(renderWindow, _aspectRatio);
        }
    }
}
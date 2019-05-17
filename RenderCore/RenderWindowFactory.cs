﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore
{
    public static class RenderWindowFactory
    {
        private static RenderWindow CreateRenderWindow(string _name, Vector2u _windowSize,
            FloatRect _viewPortRect)
        {
            VideoMode videoMode = new VideoMode(_windowSize.X, _windowSize.Y);
            RenderWindow window = new RenderWindow(videoMode, _name);

            return window;
        }

        public static RenderWindow CreateRenderWindow(string _name, Vector2u _windowSize)
        {
            FloatRect viewPortRect = new FloatRect(0, 0, 1.0f, 1.0f);

            return CreateRenderWindow(_name, _windowSize, viewPortRect);
        }
    }
}
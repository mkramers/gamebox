﻿using SFML.Graphics;

namespace RenderCore.Render
{
    public static class RenderWindowExtensions
    {
        public static void SetViewport(this SFML.Graphics.RenderWindow _renderWindow, FloatRect _viewPort)
        {
            View renderWindowView = _renderWindow.GetView();
            renderWindowView.Viewport = _viewPort;
        }
    }
}
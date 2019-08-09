using System;
using SFML.Graphics;
using SFML.System;

namespace RenderBox.New
{
    public static class WindowResizeUtilities
    {
        public static FloatRect GetViewPort(Vector2u _windowSize, float _aspectRatio)
        {
            float windowAspectRatio = (float) _windowSize.X / _windowSize.Y;
            if (windowAspectRatio <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(windowAspectRatio), "negative aspect ratio not supported");
            }

            FloatRect viewPort = new FloatRect(0, 0, 1, 1);

            if (windowAspectRatio > _aspectRatio)
            {
                float xPadding = (windowAspectRatio - _aspectRatio) / 2.0f;
                viewPort = new FloatRect(xPadding / 2.0f, 0, 1 - xPadding, 1);
            }
            else if (windowAspectRatio < _aspectRatio)
            {
                float yPadding = (_aspectRatio - windowAspectRatio) / 2.0f;
                viewPort = new FloatRect(0, yPadding / 2.0f, 0, 1 - yPadding);
            }
            else if (Math.Abs(windowAspectRatio - _aspectRatio) < 0.0001f)
            {
                viewPort = new FloatRect(0, 0, 1, 1);
            }

            return viewPort;
        }
    }
}
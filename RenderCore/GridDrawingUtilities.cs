using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class GridDrawingUtilities
    {
        public static IEnumerable<Shape> GetGridDrawableFromView(View _view)
        {
            Vector2 viewSize = _view.Size.GetVector2();
            Vector2 position = _view.Center.GetVector2() - viewSize / 2.0f;

            int rows = (int) Math.Round(viewSize.X);
            int columns = (int) Math.Round(viewSize.Y);

            IEnumerable<Shape> shapes = ShapeFactory.GetGridShapes(rows, columns, viewSize, 0.05f, position);
            return shapes;
        }
    }
}
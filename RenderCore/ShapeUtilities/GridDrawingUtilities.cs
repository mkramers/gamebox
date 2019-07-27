using System;
using System.Collections.Generic;
using System.Numerics;
using LibExtensions;
using RenderCore.Drawable;
using SFML.Graphics;

namespace RenderCore.ShapeUtilities
{
    public static class GridDrawingUtilities
    {
        public static IEnumerable<VertexArrayShape> GetGridDrawableFromView(View _view, float _lineThickness,
            Vector2 _cellSize)
        {
            Vector2 viewSize = _view.Size.GetVector2();
            Vector2 position = _view.Center.GetVector2() - viewSize / 2.0f;

            int rows = (int) Math.Round(viewSize.Y / _cellSize.X);
            int columns = (int) Math.Round(viewSize.X / _cellSize.Y);

            IEnumerable<VertexArrayShape> shapes =
                ShapeFactory.GetGridShapes(rows, columns, viewSize, _lineThickness, position);
            return shapes;
        }
    }
}
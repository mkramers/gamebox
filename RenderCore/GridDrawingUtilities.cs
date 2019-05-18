using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class GridDrawingUtilities
    {
        public static MultiDrawable<Shape> GetGridDrawableFromView(View _view)
        {
            Vector2 viewSize = _view.Size.GetVector2();
            Vector2 position = _view.Center.GetVector2() - viewSize / 2.0f;

            int rows = (int) Math.Round(viewSize.Y);
            int columns = (int) Math.Round(viewSize.X);

            IEnumerable<Shape> shapes = ShapeFactory.GetGridShapes(rows, columns, viewSize, 0.05f, position);

            MultiDrawable<Shape> gridDrawable= new MultiDrawable<Shape>(shapes.Select(_shape => new Drawable<Shape>(_shape)));
            return gridDrawable;
        }
    }
}
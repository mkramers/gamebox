﻿extern alias CoreCompatSystemDrawing;
using System.Collections.Generic;
using Common.Grid;
using CoreCompatSystemDrawing::System.Drawing;
using GameResources.Attributes;

namespace GameResources.Converters
{
    public static class BitmapToGridConverter
    {
        public static Grid<ComparableColor> GetColorGridFromBitmap(Bitmap _bitmap)
        {
            List<ComparableColor> cells = new List<ComparableColor>();

            for (int y = 0; y < _bitmap.Height; y++)
            {
                for (int x = 0; x < _bitmap.Width; x++)
                {
                    Color color = _bitmap.GetPixel(x, y);

                    cells.Add(new ComparableColor(color));
                }
            }

            Grid<ComparableColor> grid = new Grid<ComparableColor>(cells, _bitmap.Height, _bitmap.Width);
            return grid;
        }
    }
}
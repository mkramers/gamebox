extern alias CoreCompatSystemDrawing;
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
            List<GridCell<ComparableColor>> gridCells = new List<GridCell<ComparableColor>>();

            for (int x = 0; x < _bitmap.Width; x++)
            {
                for (int y = 0; y < _bitmap.Height; y++)
                {
                    Color color = _bitmap.GetPixel(x, y);

                    GridCell<ComparableColor> gridCell = new GridCell<ComparableColor>(x, y, new ComparableColor(color));
                    gridCells.Add(gridCell);
                }
            }

            Grid<ComparableColor> grid = new Grid<ComparableColor>(gridCells);
            return grid;
        }
    }
}
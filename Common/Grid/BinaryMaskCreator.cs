using System;
using System.Collections.Generic;

namespace Common.Grid
{
    public static class BinaryMaskCreator
    {
        public static Grid<bool> CreateBinaryMask<T>(Grid<T> _grid, T _threshold) where T : IComparable
        {
            GridBounds gridBounds = _grid.GetGridBounds();

            List<GridCell<bool>> gridCells = new List<GridCell<bool>>(gridBounds.Area);

            for (int y = gridBounds.MinY; y <= gridBounds.MaxY; y++)
            {
                for (int x = gridBounds.MinX; x <= gridBounds.MaxX; x++)
                {
                    IComparable cellValue = _grid[x, y];

                    bool isSet = cellValue.CompareTo(_threshold) <= 0;

                    GridCell<bool> gridCell = new GridCell<bool>(x, y, isSet);
                    gridCells.Add(gridCell);
                }
            }

            Grid<bool> grid = new Grid<bool>(gridCells);
            return grid;
        }
    }
}
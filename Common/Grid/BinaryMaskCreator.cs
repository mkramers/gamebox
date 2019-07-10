using System;
using System.Collections.Generic;

namespace Common.Grid
{
    public static class BinaryMaskCreator
    {
        public static Grid<bool> CreateBinaryMask<T>(Grid<T> _grid, T _threshold) where T : IComparable
        {
            List<bool> gridCells = new List<bool>();

            for (int y = 0; y < _grid.Rows; y++)
            {
                for (int x = 0; x < _grid.Columns; x++)
                {
                    IComparable cellValue = _grid[x, y];

                    bool isSet = cellValue.CompareTo(_threshold) <= 0;

                    gridCells.Add(isSet);
                }
            }

            Grid<bool> grid = new Grid<bool>(gridCells, _grid.Rows, _grid.Columns);
            return grid;
        }
    }
}
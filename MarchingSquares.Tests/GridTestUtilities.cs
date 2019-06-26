using System;
using System.Collections.Generic;
using Common.Grid;

namespace MarchingSquares.Tests
{
    public static class GridTestUtilities
    {
        public static Grid<T> CreateGrid<T>(int _halfSize, Func<int, int, T> _valueGenerator) where T : IComparable
        {
            IList<GridCell<T>> gridCells = new List<GridCell<T>>();
            for (int x = 0; x < _halfSize * 2; x++)
            {
                for (int y = 0; y < _halfSize * 2; y++)
                {
                    T value = _valueGenerator(x, y);
                    gridCells.Add(new GridCell<T>(x, y, value));
                }
            }

            Grid<T> grid = new Grid<T>(gridCells);
            return grid;
        }

        public static Grid<T> CreateSquareGridFromArray<T>(IReadOnlyList<T> _values) where T : IComparable
        {
            if (Math.Abs(Math.Sqrt(_values.Count / 2.0f) % 1) < 0.000001f)
            {
                throw new Exception("incorrect size of array");
            }

            int sideLength = (int)Math.Sqrt(_values.Count);

            IList<GridCell<T>> gridCells = new List<GridCell<T>>();
            for (int y = 0; y < sideLength; y++)
            {
                for (int x = 0; x < sideLength; x++)
                {
                    T value = _values[y * sideLength + x];
                    gridCells.Add(new GridCell<T>(x, y, value));
                }
            }

            Grid<T> grid = new Grid<T>(gridCells);
            return grid;
        }
    }
}
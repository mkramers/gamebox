using System;
using System.Collections.Generic;
using Common.Grid;

namespace MarchingSquares.Tests
{
    public static class GridTestUtilities
    {
        public static Grid<T> CreateGrid<T>(int _halfSize, Func<int, int, T> _valueGenerator) where T : IComparable
        {
            int size = _halfSize * 2;

            IList<T> cells = new List<T>();
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    T value = _valueGenerator(x, y);
                    cells.Add(value);
                }
            }

            Grid<T> grid = new Grid<T>(cells, size, size);
            return grid;
        }

        public static Grid<T> CreateSquareGridFromArray<T>(IReadOnlyList<T> _values) where T : IComparable
        {
            if (Math.Abs(Math.Sqrt(_values.Count / 2.0f) % 1) < 0.000001f)
            {
                throw new Exception("incorrect size of array");
            }

            int sideLength = (int) Math.Sqrt(_values.Count);

            IList<T> gridCells = new List<T>();
            for (int y = 0; y < sideLength; y++)
            {
                for (int x = 0; x < sideLength; x++)
                {
                    T value = _values[y * sideLength + x];
                    gridCells.Add(value);
                }
            }

            Grid<T> grid = new Grid<T>(gridCells, sideLength, sideLength);
            return grid;
        }
    }
}
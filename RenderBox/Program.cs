using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Common.Geometry;
using Common.VertexObject;
using SFML.System;

namespace RenderBox
{
    internal static class Program
    {
        private static void Main()
        {
            const float aspectRatio = 1.0f;

            RenderBox renderBox = new RenderBox("RenderBox", new Vector2u(800, 800), aspectRatio);
            renderBox.StartLoop();
        }
    }

    public interface IVertexObjectGenerator
    {
        IVertexObject Generate();
    }

    public class GridCell<T>
    {
        public GridCell(int _x, int _y, T _value)
        {
            X = _x;
            Y = _y;
            Value = _value;
        }

        public int X { get; }
        public int Y { get; }
        public T Value { get; }
    }

    public class Grid<T> : ReadOnlyCollection<GridCell<T>>
    {
        public Grid(IList<GridCell<T>> _cells) : base(_cells)
        {
            if (!IsRectangle(this))
            {
                throw new InvalidDataException("Grid is not a rectangle!");
            }
        }

        public T this[int _x, int _y]
        {
            get
            {
                Debug.Assert(CellExists(_x, _y));

                GridCell<T> item = this.First(_cell => _cell.X == _x && _cell.Y == _y);
                return item.Value;
            }
        }

        public bool CellExists(int _x, int _y)
        {
            bool cellExists = this.Count(_cell => _cell.X == _x && _cell.Y == _y) == 1;
            return cellExists;
        }

        public bool RowExists(int _y)
        {
            bool rowExists = this.Any(_cell => _cell.Y == _y);
            return rowExists;
        }

        public bool ColumnExists(int _y)
        {
            bool rowExists = this.Any(_cell => _cell.Y == _y);
            return rowExists;
        }

        public bool IsRectangle(Grid<T> _grid)
        {
            GridBounds gridBounds = GridBounds.GetGridBounds(_grid);

            bool isValid = gridBounds.SizeX > 0 && gridBounds.SizeY > 0;

            for (int y = gridBounds.MinY; y <= gridBounds.MaxY; y++)
            {
                for (int x = gridBounds.MinX; x <= gridBounds.MaxX; x++)
                {
                    if (!_grid.CellExists(x, y))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (!isValid)
                {
                    break;
                }
            }

            return isValid;
        }
    }

    public class GridBounds
    {
        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }

        public int SizeX => MaxX - MaxY + 1;
        public int SizeY => MaxY - MaxX + 1;
        public int Area => SizeX * SizeY;

        private GridBounds(int _minX, int _maxX, int _minY, int _maxY)
        {
            Debug.Assert(_maxX - _minX >= 0);
            Debug.Assert(_maxY - _minY >= 0);

            MinX = _minX;
            MaxX = _maxX;
            MinY = _minY;
            MaxY = _maxY;
        }

        public static GridBounds GetGridBounds<T>(Grid<T> _grid)
        {
            int minX = _grid.Min(_cell => _cell.X);
            int maxX = _grid.Max(_cell => _cell.X);

            int minY = _grid.Min(_cell => _cell.Y);
            int maxY = _grid.Max(_cell => _cell.Y);

            GridBounds gridBounds = new GridBounds(minX, maxX, minY, maxY);
            return gridBounds;
        }
    }

    public interface IBitmaskCreator<T, T0>
    {
        Grid<T0> CreateBitmask(Grid<T> _grid, T _threshold);
    }
    
    public class FloatBitmaskCreator : IBitmaskCreator<IComparable, bool>
    {
        public Grid<bool> CreateBitmask(Grid<IComparable> _grid, IComparable _threshold)
        {
            GridBounds gridBounds = GridBounds.GetGridBounds(_grid);

            int gridArea = (gridBounds.SizeX - 1) * (gridBounds.SizeY - 1);

            List<GridCell<bool>> gridCells = new List<GridCell<bool>>(gridArea);

            for (int y = gridBounds.MinY; y < gridBounds.MaxY - 1; y++)
            {
                for (int x = gridBounds.MinX; x < gridBounds.MaxX - 1; x++)
                {
                    IComparable cellValue = _grid[x, y];

                    bool isSet = cellValue.CompareTo(_threshold) > 0;

                    GridCell<bool> gridCell = new GridCell<bool>(x, y, isSet);
                    gridCells.Add(gridCell);
                }
            }

            Grid<bool> grid = new Grid<bool>(gridCells);
            return grid;
        }
    }

    public class MarchingSquaresGenerator : IVertexObjectGenerator
    {
        private readonly Polygon m_polygon;

        public MarchingSquaresGenerator(Grid<float> _grid)
        {
            List<LineSegment> lines = new List<LineSegment>();
            foreach (GridCell<float> gridCell in _grid)
            {
                LineSegment line = ClassifyCell(gridCell, _grid);
                lines.Add(line);
            }

            m_polygon = new Polygon(0);
        }

        private LineSegment ClassifyCell(GridCell<float> _gridCell, Grid<float> _grid)
        {

            LineSegment lineSegment = null;

            return lineSegment;
        }

        public IVertexObject Generate()
        {
            return m_polygon;
        }
    }
}
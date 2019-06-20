using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
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

        private bool CellExists(int _x, int _y)
        {
            bool cellExists = this.Count(_cell => _cell.X == _x && _cell.Y == _y) == 1;
            return cellExists;
        }

        private static bool IsRectangle(Grid<T> _grid)
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
        private GridBounds(int _minX, int _maxX, int _minY, int _maxY)
        {
            Debug.Assert(_maxX - _minX >= 0);
            Debug.Assert(_maxY - _minY >= 0);

            MinX = _minX;
            MaxX = _maxX;
            MinY = _minY;
            MaxY = _maxY;
        }

        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }

        public int SizeX => MaxX - MaxY + 1;
        public int SizeY => MaxY - MaxX + 1;
        public int Area => SizeX * SizeY;

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

    public static class BinaryMaskCreator
    {
        public static Grid<bool> CreateBinaryMask<T>(Grid<T> _grid, T _threshold) where T : IComparable
        {
            GridBounds gridBounds = GridBounds.GetGridBounds(_grid);

            List<GridCell<bool>> gridCells = new List<GridCell<bool>>(gridBounds.Area);

            for (int y = gridBounds.MinY; y < gridBounds.MaxY; y++)
            {
                for (int x = gridBounds.MinX; x < gridBounds.MaxX; x++)
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

    public static class MarchingSquaresClassifier
    {
        public static Grid<byte> ClassifyCells(Grid<bool> _binaryMask)
        {
            GridBounds gridBounds = GridBounds.GetGridBounds(_binaryMask);

            int area = (gridBounds.SizeX - 1) * (gridBounds.SizeY - 1);

            List<GridCell<byte>> gridCells = new List<GridCell<byte>>(area);

            for (int y = gridBounds.MinY; y < gridBounds.MaxY - 1; y++)
            {
                for (int x = gridBounds.MinX; x < gridBounds.MaxX - 1; x++)
                {
                    GridCell<byte> classifiedCell = ClassifyCell(_binaryMask, x, y);
                    gridCells.Add(classifiedCell);
                }
            }

            Grid<byte> grid = new Grid<byte>(gridCells);
            return grid;
        }

        private static GridCell<byte> ClassifyCell(Grid<bool> _binaryMask, int _x, int _y)
        {
            bool cellValueA = _binaryMask[_x, _y];
            bool cellValueB = _binaryMask[_x + 1, _y];
            bool cellValueC = _binaryMask[_x + 1, _y + 1];
            bool cellValueD = _binaryMask[_x, _y + 1];

            byte byteFlag = 0;
            if (cellValueA)
            {
                byteFlag |= 1 << 3;
            }

            if (cellValueB)
            {
                byteFlag |= 1 << 2;
            }

            if (cellValueC)
            {
                byteFlag |= 1 << 1;
            }

            if (cellValueD)
            {
                byteFlag |= 1 << 0;
            }

            GridCell<byte> classifiedCell = new GridCell<byte>(_x, _y, byteFlag);
            return classifiedCell;
        }
    }

    public static class MarchingSquaresPolygonGenerator
    {
        private static Dictionary<byte, IVertexObject> SegmentLookupTable =>
            new Dictionary<byte, IVertexObject>
            {
                {0, null},
                {1, new LineSegment(new Vector2(0, 0.5f), new Vector2(0.5f, 1.0f))},
                {2, new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))},
                {3, new LineSegment(new Vector2(0, 0.5f), new Vector2(1.0f, 0.5f))},

                {4, new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))},
                {
                    5, new LineSegmentCollection(new[]
                    {
                        new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f)),
                        new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))
                    })
                },
                {6, new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f))},
                {7, new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f))},

                {8, new LineSegment(new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.5f))},
                {9, new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f))},
                {
                    10, new LineSegmentCollection(new[]
                    {
                        new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f)),
                        new LineSegment(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f))
                    })
                },
                {11, new LineSegment(new Vector2(0.5f, 0.5f), new Vector2(1.0f, 0.0f))},

                {12, new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(1.0f, 0.5f))},
                {13, new LineSegment(new Vector2(0.5f, 1.0f), new Vector2(1.0f, 0.5f))},
                {14, new LineSegment(new Vector2(0.0f, 0.5f), new Vector2(0.5f, 1.0f))},
                {15, null}
            };

        public static IEnumerable<IVertexObject> GeneratePolygons(Grid<byte> _classifiedCells)
        {
            Polygon polygon = new Polygon(1);

            foreach (GridCell<byte> classifiedCell in _classifiedCells)
            {
                IVertexObject lines = SegmentLookupTable[classifiedCell.Value];
                if (lines != null)
                {
                    polygon.AddRange(lines);
                }
            }

            return new[] {polygon};
        }
    }

    public class MarchingSquaresGenerator<T> where T : IComparable
    {
        private readonly Grid<T> m_grid;
        private readonly T m_threshold;

        public MarchingSquaresGenerator(Grid<T> _grid, T _threshold)
        {
            m_grid = _grid;
            m_threshold = _threshold;
        }

        public IEnumerable<IVertexObject> Generate()
        {
            Grid<bool> binaryMask = BinaryMaskCreator.CreateBinaryMask(m_grid, m_threshold);

            Grid<byte> classifiedCells = MarchingSquaresClassifier.ClassifyCells(binaryMask);

            IEnumerable<IVertexObject> polygons = MarchingSquaresPolygonGenerator.GeneratePolygons(classifiedCells);
            return polygons;
        }
    }
}
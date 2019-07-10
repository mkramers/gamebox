using System.Diagnostics;
using System.Linq;

namespace Common.Grid
{
    public class GridBounds
    {
        public GridBounds(int _minX, int _maxX, int _minY, int _maxY)
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

        public int Rows => MaxX - MinX + 1;
        public int Columns => MaxY - MinY + 1;
        public int Area => Rows * Columns;
    }

    public static class GridExtensions
    {
        public static GridBounds GetGridBounds<T>(this Grid<T> _grid)
        {
            int minX = _grid.Min(_cell => _cell.X);
            int maxX = _grid.Max(_cell => _cell.X);

            int minY = _grid.Min(_cell => _cell.Y);
            int maxY = _grid.Max(_cell => _cell.Y);

            GridBounds gridBounds = new GridBounds(minX, maxX, minY, maxY);
            return gridBounds;
        }
    }
}
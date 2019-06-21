using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Common.Grid
{
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
}
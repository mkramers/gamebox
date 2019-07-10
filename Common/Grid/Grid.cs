using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Common.Grid
{
    public class Grid<T> : ReadOnlyCollection<T>
    {
        public int Rows { get; }
        public int Columns { get; }

        public Grid(IList<T> _cells, int _rows, int _columns) : base(_cells)
        {
            if (_cells.Count != _rows * _columns)
            {
                throw new InvalidDataException($"Grid is incomplete. Expected a {_columns} x {_rows} grid input");
            }

            Rows = _rows;
            Columns = _columns;
        }

        public T this[int _x, int _y]
        {
            get
            {
                int index = GetIndex(_x, _y);
                Debug.Assert(IsIndexValid(index));

                T item = this[index];
                return item;
            }
        }

        private bool IsIndexValid(int _index)
        {
            return _index >= 0 && _index < Count;
        }

        private int GetIndex(int _x, int _y)
        {
            int index = _x + _y * Rows;
            Debug.Assert(IsIndexValid(index));

            return index;
        }
    }
}
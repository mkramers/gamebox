using System;
using System.Text;

namespace Common.Grid
{
    public static class GridExtensions
    {
        public static string GetGridDisplayText<T>(this Grid<T> _grid)
        {
            StringBuilder displayText = new StringBuilder();

            for (int y = 0; y < _grid.Columns; y++)
            {
                for (int x = 0; x < _grid.Rows; x++)
                {
                    T value = _grid[x, y];
                    displayText.Append($"{value}{(x < _grid.Rows - 1 ? "\t" : "\n")}");
                }
            }

            return displayText.ToString();
        }

        public static void PrintGrid<T>(this Grid<T> _grid, string _name)
        {
            string separator = new string('=', 6);
            Console.WriteLine($"{separator}\n{_name}\n{separator}\n{_grid.GetGridDisplayText()}");
        }
    }
}
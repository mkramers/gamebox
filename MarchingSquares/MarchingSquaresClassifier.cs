using System.Collections.Generic;
using Common.Grid;

namespace MarchingSquares
{
    public static class MarchingSquaresClassifier
    {
        public static Grid<byte> ClassifyCells(Grid<bool> _binaryMask)
        {
            int rows = _binaryMask.Rows - 1;
            int columns = _binaryMask.Columns - 1;

            List<byte> cells = new List<byte>(rows * columns);
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    byte classifiedCell = ClassifyCell(_binaryMask, x, y);
                    cells.Add(classifiedCell);
                }
            }

            Grid<byte> grid = new Grid<byte>(cells, rows, columns);
            return grid;
        }

        private static byte ClassifyCell(Grid<bool> _binaryMask, int _x, int _y)
        {
            bool GetCellValue(int _xPos, int _yPos)
            {
                return _binaryMask[_xPos, _yPos];
            }

            bool cellValueA = GetCellValue(_x, _y);
            bool cellValueB = GetCellValue(_x + 1, _y);
            bool cellValueC = GetCellValue(_x + 1, _y + 1);
            bool cellValueD = GetCellValue(_x, _y + 1);

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

            return byteFlag;
        }
    }
}
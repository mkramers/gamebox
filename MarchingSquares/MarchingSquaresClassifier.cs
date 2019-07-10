using System.Collections.Generic;
using Common.Grid;

namespace MarchingSquares
{
    public static class MarchingSquaresClassifier
    {
        public static Grid<byte> ClassifyCells(Grid<bool> _binaryMask)
        {
            GridBounds gridBounds = _binaryMask.GetGridBounds();

            int area = (gridBounds.SizeX - 1) * (gridBounds.SizeY - 1);

            List<GridCell<byte>> gridCells = new List<GridCell<byte>>(area);

            for (int y = gridBounds.MinY; y < gridBounds.MaxY; y++)
            {
                for (int x = gridBounds.MinX; x < gridBounds.MaxX; x++)
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
            bool GetCellValue(int _xPos, int _yPos)
            {
                return _binaryMask.CellExists(_xPos, _yPos) && _binaryMask[_xPos, _yPos];
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

            GridCell<byte> classifiedCell = new GridCell<byte>(_x, _y, byteFlag);
            return classifiedCell;
        }
    }
}

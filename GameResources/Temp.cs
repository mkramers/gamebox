extern alias CoreCompatSystemDrawing;
using System;
using System.Collections.Generic;
using Common.Geometry;
using Common.Grid;
using CoreCompatSystemDrawing::System.Drawing;
using GameResources.Attributes;
using GameResources.Converters;
using MarchingSquares;

namespace GameResources
{
    public static class Temp
    {
        public static IEnumerable<LineSegment> Do(string _fileName)
        {
            Bitmap bitmap = new Bitmap(_fileName);

            ComparableColor colorThreshold = new ComparableColor(Color.FromArgb(0, 0, 0, 0));

            Grid<ComparableColor> grid = BitmapToGridConverter.GetColorGridFromBitmap(bitmap);

            grid.PrintGrid(nameof(grid));

            Grid<bool> binaryMask = BinaryMaskCreator.CreateBinaryMask(grid, colorThreshold);

            binaryMask.PrintGrid(nameof(binaryMask));

            Grid<byte> classifiedCells = MarchingSquaresClassifier.ClassifyCells(binaryMask);

            classifiedCells.PrintGrid(nameof(classifiedCells));

            IEnumerable<LineSegment> lineSegments = MarchingSquaresPolygonGenerator.GetLineSegments(classifiedCells);
            return lineSegments;
        }
    }
}
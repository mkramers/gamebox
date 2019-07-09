extern alias CoreCompatSystemDrawing;
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

            Grid<bool> binaryMask = BinaryMaskCreator.CreateBinaryMask(grid, colorThreshold);

            Grid<byte> classifiedCells = MarchingSquaresClassifier.ClassifyCells(binaryMask);

            IEnumerable<LineSegment> lineSegments = MarchingSquaresPolygonGenerator.GetLineSegments(classifiedCells);
            return lineSegments;
        }
    }
}
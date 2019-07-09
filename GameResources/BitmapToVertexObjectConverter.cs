extern alias CoreCompatSystemDrawing;
using System.Collections.Generic;
using Common.Grid;
using Common.VertexObject;
using CoreCompatSystemDrawing::System.Drawing;
using GameResources.Attributes;
using GameResources.Converters;
using MarchingSquares;

namespace GameResources
{
    public static class BitmapToVertexObjectConverter
    {
        public static IEnumerable<IVertexObject> GetVertexObjectsFromBitmap(Bitmap _bitmap, ComparableColor _threshold)
        {
            Grid<ComparableColor> grid = BitmapToGridConverter.GetColorGridFromBitmap(_bitmap);

            MarchingSquaresGenerator<ComparableColor> marchingSquares =
                new MarchingSquaresGenerator<ComparableColor>(grid, _threshold);

            IVertexObjectsGenerator generator = new HeadToTailGenerator();
            IEnumerable<IVertexObject> polygons = marchingSquares.Generate(generator);
            return polygons;
        }
    }
}
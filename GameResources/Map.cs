extern alias CoreCompatSystemDrawing;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common.Geometry;
using Common.Grid;
using Common.VertexObject;
using GameResources.Attributes;
using GameResources.Converters;
using MarchingSquares;
using Bitmap = CoreCompatSystemDrawing::System.Drawing.Bitmap;
using Color = CoreCompatSystemDrawing::System.Drawing.Color;

namespace GameResources
{
    public class Map
    {
        public Map(string _mapName, MapLayer _collisionLayer, MapLayer _sceneLayer)
        {
            MapName = _mapName;
            CollisionLayer = _collisionLayer;
            SceneLayer = _sceneLayer;
        }

        private string MapName { get; }
        private MapLayer CollisionLayer { get; }
        public MapLayer SceneLayer { get; }

        public IEnumerable<IVertexObject> GetCollisionVertexObjects()
        {
            Bitmap bitmap = new Bitmap(CollisionLayer.FileName);

            ComparableColor colorThreshold = new ComparableColor(Color.FromArgb(0, 0, 0, 0));

            IEnumerable<IVertexObject> polygons = BitmapToVertexObjectConverter.GetVertexObjectsFromBitmap(bitmap, colorThreshold);
            return polygons;
        }

        public static IEnumerable<LineSegment> Do(string _fileName)
        {
            Bitmap bitmap = new Bitmap(_fileName);

            ComparableColor colorThreshold = new ComparableColor(Color.FromArgb(0, 0, 0, 0));

            Grid<ComparableColor> grid = BitmapToGridConverter.GetColorGridFromBitmap(bitmap);

            MarchingSquaresGenerator<ComparableColor> marchingSquares =
                new MarchingSquaresGenerator<ComparableColor>(grid, colorThreshold);

            Grid<bool> binaryMask = BinaryMaskCreator.CreateBinaryMask(grid, colorThreshold);

            Grid<byte> classifiedCells = MarchingSquaresClassifier.ClassifyCells(binaryMask);

            IEnumerable<LineSegment> lineSegments = MarchingSquaresPolygonGenerator.GetLineSegments(classifiedCells);

            IVertexObjectsGenerator generator = new HeadToTailGenerator();
            IEnumerable<IVertexObject> polygons = generator.GetVertexObjects(lineSegments);
            return lineSegments;
        }
    }
}
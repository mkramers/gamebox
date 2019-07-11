extern alias CoreCompatSystemDrawing;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public Grid<ComparableColor> GetCollisionGrid()
        {
            Bitmap bitmap = new Bitmap(CollisionLayer.FileName);

            Grid<ComparableColor> grid = BitmapToGridConverter.GetColorGridFromBitmap(bitmap);
            return grid;
        }
    }
}
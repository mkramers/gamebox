extern alias CoreCompatSystemDrawing;
using Common.Grid;
using CoreCompatSystemDrawing::System.Drawing;
using GameResources.Attributes;
using GameResources.Converters;

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
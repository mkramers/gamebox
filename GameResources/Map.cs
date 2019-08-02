extern alias CoreCompatSystemDrawing;
using System.Collections.Generic;
using System.Diagnostics;
using Common.Grid;
using CoreCompatSystemDrawing::System.Drawing;
using GameResources.Attributes;
using GameResources.Converters;

namespace GameResources
{
    public class Map : Dictionary<string, MapLayer>
    {
        public Map(IEnumerable<MapLayer> _layers)
        {
            foreach (MapLayer mapLayer in _layers)
            {
                Add(mapLayer.Name, mapLayer);
            }
        }
    }

    public static class MapExtensions
    {
        public static Grid<ComparableColor> GetCollisionGrid(this Map _map)
        {
            Debug.Assert(_map.ContainsKey("collision"));

            MapLayer collisionLayer = _map["collision"];

            Bitmap bitmap = new Bitmap(collisionLayer.FilePath);

            Grid<ComparableColor> grid = BitmapToGridConverter.GetColorGridFromBitmap(bitmap);
            return grid;
        }
    }
}
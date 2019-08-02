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
        public Grid<ComparableColor> GetCollisionGrid()
        {
            Debug.Assert(ContainsKey("collision"));

            MapLayer collisionLayer = this["collision"];

            Bitmap bitmap = new Bitmap(collisionLayer.FilePath);

            Grid<ComparableColor> grid = BitmapToGridConverter.GetColorGridFromBitmap(bitmap);
            return grid;
        }
    }
}
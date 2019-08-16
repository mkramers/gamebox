extern alias CoreCompatSystemDrawing;
using System.Diagnostics;
using Common.Grid;
using CoreCompatSystemDrawing::System.Drawing;
using GameResources.Attributes;
using GameResources.Converters;

namespace GameResources
{
    public static class SpriteLayersExtensions
    {
        public static Grid<ComparableColor> GetCollisionGrid(this SpriteLayers _spriteLayers)
        {
            Debug.Assert(_spriteLayers.ContainsKey("collision"));

            SpriteLayer collisionLayer = _spriteLayers["collision"];

            Bitmap bitmap = new Bitmap(collisionLayer.FilePath);

            Grid<ComparableColor> grid = BitmapToGridConverter.GetColorGridFromBitmap(bitmap);
            return grid;
        }
    }
}
extern alias CoreCompatSystemDrawing;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common.VertexObject;
using GameResources.Attributes;
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
    }
}
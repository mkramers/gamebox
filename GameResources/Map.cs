using System;
using Common.VertexObject;

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

        public string MapName { get; }
        public MapLayer CollisionLayer { get; }
        public MapLayer SceneLayer { get; }

        public IVertexObject GetCollisionVertexObject()
        {
            throw new NotImplementedException();
        }
    }
}
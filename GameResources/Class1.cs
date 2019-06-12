using System;
using System.Collections.Generic;
using Common.Math;

namespace GameResources
{
    public class MapLayer
    {
        public MapLayer(IntSize _size, string _fileName)
        {
            Size = _size;
            FileName = _fileName;
        }

        public IntSize Size { get; }
        public string FileName { get; }
    }

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
    }
}

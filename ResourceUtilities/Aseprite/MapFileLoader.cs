using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Common.Geometry;
using GameResources;

namespace ResourceUtilities.Aseprite
{
    public class MapFileLoader
    {
        public Map LoadMapFromFile(SpriteSheetFile _spriteSheetFile)
        {
            SpriteSheet spriteSheet = _spriteSheetFile.SpriteSheet;

            string mapName = GetMapName(spriteSheet);

            MapLayer collisionMapLayer = LoadMapLayer(_spriteSheetFile, "collision");
            MapLayer sceneMapLayer = LoadMapLayer(_spriteSheetFile, "scene");

            return new Map(mapName, collisionMapLayer, sceneMapLayer);
        }

        private static string GetMapName(SpriteSheet _spriteSheet)
        {
            Meta meta = _spriteSheet.Meta;

            string mapName = Path.GetFileNameWithoutExtension(meta.Image);
            return mapName;
        }

        private static IntSize GetLayerSize(SpriteSheet _spriteSheet)
        {
            IntSize size = _spriteSheet.Meta.Size;
            return size;
        }

        private string GetLayerFileName(string _layerName, SpriteSheet _spriteSheet)
        {
            if (!LayerExists(_spriteSheet, _layerName))
            {
                throw new KeyNotFoundException($"layer {_layerName} not found");
            }

            string mapName = GetMapName(_spriteSheet);
            string extension = Path.GetExtension(_spriteSheet.Meta.Image);
            string layerFileName = Path.ChangeExtension($"{mapName}-{_layerName}", extension);
            return layerFileName;
        }

        private string GetLayerFilePath(SpriteSheetFile _spriteSheetFile, string _layerFileName)
        {
            string spriteDirectory = Path.GetDirectoryName(_spriteSheetFile.FilePath);
            if (spriteDirectory == null)
            {
                throw new NullReferenceException(nameof(spriteDirectory));
            }

            string layerFilePath = Path.Combine(spriteDirectory, _layerFileName);
            return layerFilePath;
        }

        private static bool LayerExists(SpriteSheet _spriteSheet, string _layerName)
        {
            Meta meta = _spriteSheet.Meta;

            return meta.Layers.Any(_layer => _layer.Name == _layerName);
        }

        private MapLayer LoadMapLayer(SpriteSheetFile _spriteSheetFile, string _layerName)
        {
            SpriteSheet spriteSheet = _spriteSheetFile.SpriteSheet;

            IntSize size = GetLayerSize(spriteSheet);

            string layerFileName = GetLayerFileName(_layerName, spriteSheet);

            string layerFilePath = GetLayerFilePath(_spriteSheetFile, layerFileName);

            if (!File.Exists(layerFilePath))
            {
                throw new KeyNotFoundException($"layer spriteSheet {layerFilePath} not found");
            }

            MapLayer mapLayer = new MapLayer(size, layerFilePath);
            return mapLayer;
        }
    }
}
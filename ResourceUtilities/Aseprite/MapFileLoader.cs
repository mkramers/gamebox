using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Geometry;
using GameResources;

namespace ResourceUtilities.Aseprite
{
    public class MapFileLoader
    {
        public SpriteLayers LoadSpriteLayersFromFile(SpriteSheetFile _spriteSheetFile)
        {
            SpriteSheet spriteSheet = _spriteSheetFile.SpriteSheet;

            List<SpriteLayer> spriteLayers = new List<SpriteLayer>();
            foreach (Layer layer in spriteSheet.Meta.Layers)
            {
                SpriteLayer spriteLayer = LoadSpriteLayer(_spriteSheetFile, layer.Name);

                spriteLayers.Add(spriteLayer);
            }

            return new SpriteLayers(spriteLayers);
        }

        private static IntSize GetLayerSize(SpriteSheet _spriteSheet)
        {
            IntSize size = _spriteSheet.Meta.Size;
            return size;
        }

        private static string GetLayerFileName(string _layerName, SpriteSheet _spriteSheet)
        {
            if (!LayerExists(_spriteSheet, _layerName))
            {
                throw new KeyNotFoundException($"layer {_layerName} not found");
            }

            string mapName = _spriteSheet.SpriteName;
            const string extension = ".png";
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

        private SpriteLayer LoadSpriteLayer(SpriteSheetFile _spriteSheetFile, string _layerName)
        {
            SpriteSheet spriteSheet = _spriteSheetFile.SpriteSheet;

            string layerFileName = GetLayerFileName(_layerName, spriteSheet);

            string layerFilePath = GetLayerFilePath(_spriteSheetFile, layerFileName);

            if (!File.Exists(layerFilePath))
            {
                throw new FileNotFoundException($"layer spriteSheet {layerFilePath} not found");
            }

            SpriteLayer spriteLayer = new SpriteLayer(_layerName, layerFilePath);
            return spriteLayer;
        }
    }
}
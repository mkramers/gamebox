using Common.Math;
using GameResources;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ResourceUtilities.Aseprite
{
    public class SpriteSheetLoader
    {
        public SpriteSheet LoadFromFile(string _path)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false
                }
            };

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };

            string fileContents =
                System.IO.File.ReadAllText(_path);

            SpriteSheet dFile2 = JsonConvert.DeserializeObject<SpriteSheet>(fileContents, settings);
            return dFile2;
        }
    }

    public class SpriteSheetFileLoader
    {
        public SpriteSheetFile LoadFromFile(string _path)
        {
            SpriteSheetLoader spriteSheetLoader = new SpriteSheetLoader();
            SpriteSheet spriteSheet = spriteSheetLoader.LoadFromFile(_path);
            return new SpriteSheetFile(spriteSheet, _path);
        }
    }

    public class SpriteSheetFile    
    {
        public SpriteSheetFile(SpriteSheet _spriteSheet, string _filePath)
        {
            SpriteSheet = _spriteSheet;
            FilePath = _filePath;
        }

        public SpriteSheet SpriteSheet { get; }
        public string FilePath { get; }
    }

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
            string layerFilePath = Path.Combine(spriteDirectory, _layerFileName);
            return layerFilePath;
        }

        private bool LayerExists(SpriteSheet _spriteSheet, string _layerName)
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

            if (!System.IO.File.Exists(layerFilePath))
            {
                throw new KeyNotFoundException($"layer spriteSheet {layerFilePath} not found");
            }

            MapLayer mapLayer = new MapLayer(size, layerFilePath);
            return mapLayer;
        }
    }

    public class SpriteSheet
    {
        [JsonProperty]
        public Frame[] Frames { get; private set; }

        [JsonProperty]
        public Meta Meta { get; private set; }
    }

    public class Frame
    {
        [JsonProperty]
        public string FileName { get; private set; }

        [JsonProperty(PropertyName = "frame")]
        public IntRect FrameRect { get; private set; }

        [JsonProperty]
        public bool Rotated { get; private set; }

        [JsonProperty]
        public bool Trimmed { get; private set; }

        [JsonProperty]
        public IntRect SpriteSourceSize { get; private set; }

        [JsonProperty]
        public IntSize SourceSize { get; private set; }

        [JsonProperty]
        public int Duration { get; private set; }
    }

    public class Meta
    {
        [JsonProperty]
        public string App { get; private set; }
        
        [JsonProperty]
        public string Version { get; private set; }

        [JsonProperty]
        public string Image { get; private set; }

        [JsonProperty]
        public string Format { get; private set; }

        [JsonProperty]
        public IntSize Size { get; private set; }

        [JsonProperty]
        public int Scale { get; private set; }

        [JsonProperty]
        public string[] FrameTags { get; private set; }

        [JsonProperty]
        public Layer[] Layers { get; private set; }

        [JsonProperty]
        public string[] Slices { get; private set; }
    }

    public class Layer
    {
        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public int Opacity { get; private set; }

        [JsonProperty]
        public string BlendMode { get; private set; }
    }
}

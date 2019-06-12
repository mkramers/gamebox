using Newtonsoft.Json;
using Common.Math;
using Newtonsoft.Json.Serialization;

namespace ResourceUtilities.Aseprite
{
    public interface IFileLoader
    {
        File LoadFromFile(string _path);
    }
    
    public class FileLoader : IFileLoader
    {
        public File LoadFromFile(string _path)
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

            File dFile2 = JsonConvert.DeserializeObject<File>(fileContents, settings);
            return dFile2;
        }
    }

    public class File
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

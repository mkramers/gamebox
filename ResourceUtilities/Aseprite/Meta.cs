using Common.Geometry;
using Newtonsoft.Json;

namespace ResourceUtilities.Aseprite
{
    public class Meta
    {
        [JsonProperty] public string App { get; private set; }

        [JsonProperty] public string Version { get; private set; }

        [JsonProperty] public string Format { get; private set; }

        [JsonProperty] public IntSize Size { get; private set; }

        [JsonProperty] public int Scale { get; private set; }

        [JsonProperty] public string[] FrameTags { get; private set; }

        [JsonProperty] public Layer[] Layers { get; private set; }

        [JsonProperty] public string[] Slices { get; private set; }
    }
}
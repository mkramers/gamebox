using Common.Math;
using Newtonsoft.Json;

namespace ResourceUtilities.Aseprite
{
    public class Frame
    {
        [JsonProperty] public string FileName { get; private set; }

        [JsonProperty(PropertyName = "frame")] public IntRect FrameRect { get; private set; }

        [JsonProperty] public bool Rotated { get; private set; }

        [JsonProperty] public bool Trimmed { get; private set; }

        [JsonProperty] public IntRect SpriteSourceSize { get; private set; }

        [JsonProperty] public IntSize SourceSize { get; private set; }

        [JsonProperty] public int Duration { get; private set; }
    }
}
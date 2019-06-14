using Newtonsoft.Json;

namespace ResourceUtilities.Aseprite
{
    public class Layer
    {
        [JsonProperty] public string Name { get; private set; }

        [JsonProperty] public int Opacity { get; private set; }

        [JsonProperty] public string BlendMode { get; private set; }
    }
}
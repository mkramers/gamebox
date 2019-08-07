using Newtonsoft.Json;

namespace ResourceUtilities.Aseprite
{
    public class SpriteSheet
    {
        public string SpriteName { get; set; }

        [JsonProperty] public Frame[] Frames { get; private set; }

        [JsonProperty] public Meta Meta { get; private set; }
    }
}
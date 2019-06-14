using Newtonsoft.Json;

namespace Common.Geometry
{
    public class IntSize
    {
        [JsonConstructor]
        public IntSize(int _width, int _height)
        {
            Width = _width;
            Height = _height;
        }

        [JsonProperty(PropertyName = "w")] public int Width { get; private set; }

        [JsonProperty(PropertyName = "h")] public int Height { get; private set; }
    }
}
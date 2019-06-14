using Newtonsoft.Json;

namespace Common.Geometry
{
    public class IntRect
    {
        [JsonConstructor]
        public IntRect(int _x, int _y, int _width, int _height)
        {
            X = _x;
            Y = _y;
            Width = _width;
            Height = _height;
        }

        [JsonProperty(PropertyName = "x")] public int X { get; private set; }

        [JsonProperty(PropertyName = "y")] public int Y { get; private set; }

        [JsonProperty(PropertyName = "w")] public int Width { get; private set; }

        [JsonProperty(PropertyName = "h")] public int Height { get; private set; }
    }
}
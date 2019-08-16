extern alias CoreCompatSystemDrawing;
using System.Collections.Generic;

namespace GameResources
{
    public class SpriteLayers : Dictionary<string, SpriteLayer>
    {
        public SpriteLayers(IEnumerable<SpriteLayer> _layers)
        {
            foreach (SpriteLayer mapLayer in _layers)
            {
                Add(mapLayer.Name, mapLayer);
            }
        }
    }
}
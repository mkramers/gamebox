using SFML.Graphics;

namespace RenderCore
{
    public class TextureMetaInfo
    {
        public TextureMetaInfo(string _resourceName, IntRect? _textureCropRect = null)
        {
            ResourceName = _resourceName;
            TextureCropRect = _textureCropRect;
        }

        public string ResourceName { get; }
        public IntRect? TextureCropRect { get; }
    }
}
using SFML.Graphics;

namespace RenderCore.TextureCache
{
    public class TextureResourceArgs : ITextureArgs
    {
        public TextureResourceArgs(string _resourceName, IntRect? _area = null)
        {
            ResourceName = _resourceName;
            Area = _area;
        }

        public IntRect? Area { get; }

        public string ResourceName { get; }

        public bool Equals(ITextureArgs _other)
        {
            TextureResourceArgs otherTextureArgs = _other as TextureResourceArgs;
            if (otherTextureArgs == null)
            {
                return false;
            }

            return Area.Equals(otherTextureArgs.Area) && ResourceName.Equals(otherTextureArgs.ResourceName);
        }

        public Texture GetTexture()
        {
            byte[] resourceData = Resource.ResourceUtilities.GetResourceData(ResourceName);

            Image image = new Image(resourceData);

            Texture texture = Area != null
                ? new Texture(image, Area.Value)
                : new Texture(image);
            return texture;
        }
    }
}
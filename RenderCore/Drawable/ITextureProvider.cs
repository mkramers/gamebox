using SFML.Graphics;

namespace RenderCore.Drawable
{
    public interface ITextureProvider
    {
        Texture GetTexture();
        void SetSize(uint _width, uint _height);
    }
}
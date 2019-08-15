using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore.Drawable
{
    public interface ITextureProvider
    {
        IEnumerable<Texture> GetTextures();
    }
}
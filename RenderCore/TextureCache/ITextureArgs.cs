using System;
using SFML.Graphics;

namespace RenderCore.TextureCache
{
    public interface ITextureArgs : IEquatable<ITextureArgs>
    {
        Texture GetTexture();
    }
}
using System;
using SFML.Graphics;

namespace RenderCore
{
    public interface ITextureArgs : IEquatable<ITextureArgs>
    {
        Texture GetTexture();
    }
}
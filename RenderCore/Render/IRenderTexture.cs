using System;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.Graphics;

namespace RenderCore.Render
{
    public interface IRenderTexture : IRenderTarget, ITextureProvider
    {
        RenderTarget GetRenderTarget();
    }
}
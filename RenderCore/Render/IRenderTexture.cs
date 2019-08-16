using RenderCore.Drawable;
using SFML.Graphics;

namespace RenderCore.Render
{
    public interface IRenderTexture : IRenderTarget, ITextureProvider
    {
        RenderTarget GetRenderTarget();
    }
}
using System;
using RenderCore.Drawable;
using RenderCore.ViewProvider;

namespace RenderCore.Render
{
    public interface ISceneProvider : ITextureProvider, IDrawableConsumer, IViewProviderConsumer, IDisposable
    {
    }
}
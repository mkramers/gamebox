using System;
using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.ViewProvider;

namespace GameCore
{
    public interface IGameProvider : IDrawableProvider, ITickableProvider, IViewProvider, IDisposable
    {
    }
}
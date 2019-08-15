using System;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;

namespace GameCore
{
    public interface IGameProvider : IDrawableProvider, ITextureProvider, ITickableProvider, IWidgetProvider, IBodyProvider, IDisposable
    {
        event EventHandler PauseGame;
        event EventHandler ResumeGame;
    }
}
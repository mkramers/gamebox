using System;
using Common.Tickable;
using RenderCore.Drawable;

namespace GameCore
{
    public interface IGameProvider : IDrawableProvider, ITickableProvider, IDisposable
    {
        event EventHandler PauseGame;
        event EventHandler ResumeGame;
    }
}
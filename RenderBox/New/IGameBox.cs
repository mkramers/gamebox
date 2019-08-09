using System;
using Common.Tickable;
using PhysicsCore;
using RenderCore.ViewProvider;
using TGUI;

namespace RenderBox.New
{
    public interface IGameBox : IDisposable
    {
        void StartLoop();
        void AddDrawableProvider(IDrawableProvider _drawableProvider);
        void AddTickable(ITickable _tickable);
        void SetViewProvider(IViewProvider _viewProvider);
        void SetIsPaused(bool _isPaused);
        void InvokeGui(Action<Gui> _guiAction);
    }
}
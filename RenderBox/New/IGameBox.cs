using System;
using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.System;
using TGUI;

namespace RenderBox.New
{
    public interface IGameBox : IDisposable
    {
        void StartLoop();
        void AddDrawableProvider(IDrawableProvider _drawableProvider);
        void SetViewProvider(IViewProvider _viewProvider);
        void SetIsPaused(bool _isPaused);
        Gui GetGui();
        Vector2u GetWindowSize();
        void AddTickableProvider(TickableProvider _tickableProvider);
    }
}
using System;
using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.System;

namespace GameCore
{
    public interface IGameBox : ILoopable, IDisposable
    {
        void AddDrawableProvider(IDrawableProvider _drawableProvider);
        void AddWidgetProvider(IWidgetProvider _widgetProvider);
        void SetViewProvider(IViewProvider _viewProvider);
        void SetIsPaused(bool _isPaused);
        Vector2u GetWindowSize();
        void AddTickableProvider(ITickableProvider _tickableProvider);
    }
}
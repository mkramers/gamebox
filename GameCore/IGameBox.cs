using System;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.System;

namespace GameCore
{
    public interface IGameBox : ILoopable, IDisposable
    {
        void AddDrawableProvider(IDrawableProvider _drawableProvider);
        void AddWidgetProvider(IWidgetProvider _widgetProvider);
        void AddBodyProvider(IBodyProvider _bodyProvider);
        void SetViewProvider(IViewProvider _viewProvider);
        void AddTextureProvider(ITextureProvider _textureProvider);
        void SetIsPaused(bool _isPaused);
        Vector2u GetWindowSize();
        void AddTickableProvider(ITickableProvider _tickableProvider);
    }
}
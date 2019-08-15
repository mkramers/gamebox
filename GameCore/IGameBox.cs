using System;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;
using SFML.System;

namespace GameCore
{
    public interface IGameBox : ILoopable, IDisposable
    {
        void AddWidgetProvider(IWidgetProvider _widgetProvider);
        void AddBodyProvider(IBodyProvider _bodyProvider);
        void SetTextureProvider(ITextureProvider _textureProvider);
        void SetIsPaused(bool _isPaused);
        Vector2u GetWindowSize();
        void AddTickableProvider(ITickableProvider _tickableProvider);
    }
}
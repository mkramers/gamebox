using System;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;

namespace GameCore
{
    public interface IGameBox : ILoopable, IWidgetConsumer, IDisposable
    {
        void AddBodyProvider(IBodyProvider _bodyProvider);
        void SetTextureProvider(ITextureProvider _textureProvider);
        void SetIsPaused(bool _isPaused);
        void AddTickableProvider(ITickableProvider _tickableProvider);
    }
}
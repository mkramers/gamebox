using System;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;

namespace GameCore
{
    public interface IGameBox : ILoopable, IWidgetConsumer, IBodyConsumer, ITickableConsumer, IDisposable
    {
        void SetTextureProvider(ITextureProvider _textureProvider);
        void SetIsPaused(bool _isPaused);
    }
}
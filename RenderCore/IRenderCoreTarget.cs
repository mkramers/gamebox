using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public interface IRenderCoreTarget : IDrawable, ITickable, IRenderObjectContainer
    {
        void SetViewProvider(IViewProvider _viewProvider);
        void SetTextureSize(Vector2u _textureSize);
    }
}
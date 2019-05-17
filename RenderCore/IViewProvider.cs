using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public interface IViewProvider : ITickable
    {
        View GetView();
        void SetParentSize(Vector2u _windowSize);
    }
}
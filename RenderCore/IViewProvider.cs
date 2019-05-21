using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public interface IViewProvider
    {
        View GetView();
        void SetSize(Vector2f _size);
        void SetCenter(Vector2f _center);
    }
}
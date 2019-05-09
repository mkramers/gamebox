using System;
using SFML.Graphics;

namespace RenderCore
{
    public interface IRenderCoreWindowWidget : IDrawable, ITickable
    {
        void SetView(View _view);
    }
}
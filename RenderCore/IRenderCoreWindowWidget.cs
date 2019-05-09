using System;
using SFML.Graphics;

namespace RenderCore
{
    public interface IRenderCoreWindowWidget : IDrawable
    {
        void SetView(View _view);
    }
}
using System;
using RenderCore.ViewProvider;
using SFML.Graphics;

namespace RenderCore.Render
{
    public interface IRenderTarget : IViewConsumer, IDisposable
    {
        void Display();
        void Clear(Color _color);
    }
}
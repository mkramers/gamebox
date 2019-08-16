using System;
using RenderCore.ViewProvider;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore.Render
{
    public interface IRenderWindowWrapper : IViewProvider, IViewConsumer
    {
        void Display();
        void Draw(Texture _texture, RenderStates _states);
        void Clear(Color _color);
        void DispatchEvents();

        event EventHandler<SizeEventArgs> Resized;
        event EventHandler Closed;
    }
}
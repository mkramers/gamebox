using System;
using RenderCore.ViewProvider;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore.Render
{
    public interface IRenderWindow : IViewProvider, IRenderTarget
    {
        void Draw(Texture _texture, RenderStates _states);
        void DispatchEvents();

        event EventHandler<SizeEventArgs> Resized;
        event EventHandler Closed;
        void ResetToDefaultView();
    }
}
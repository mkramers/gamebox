using System;
using SFML.Graphics;

namespace RenderCore.Render
{
    public interface IRenderTexture : IDisposable
    {
        void Clear(Color _color);
        void SetView(View _view);
        void Display();
        RenderTarget GetRenderTarget();
        Texture GetTexture();
        void SetSize(uint _width, uint _height);
    }
}
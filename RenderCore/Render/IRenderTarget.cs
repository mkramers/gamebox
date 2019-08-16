using SFML.Graphics;

namespace RenderCore.Render
{
    public interface IRenderTarget
    {
        void Display();
        void Clear(Color _color);
    }
}
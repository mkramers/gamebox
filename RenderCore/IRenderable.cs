using System.Collections;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore
{
    public interface IRenderable
    {
        void Draw(RenderTarget _renderTarget);
    }
}
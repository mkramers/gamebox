using System.Collections;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore
{
    public interface IBodyRepresentation
    {
        void Draw(RenderTarget _renderTarget);
    }
}
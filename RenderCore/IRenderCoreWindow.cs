using SFML.Graphics;

namespace RenderCore
{
    public interface IRenderCoreWindow
    {
        void StartRenderLoop();
        void DrawScene(RenderWindow _renderWindow);
    }
}
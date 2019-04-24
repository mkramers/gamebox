using SFML.Graphics;

namespace RenderCore
{
    public class RenderCoreWindow : RenderCoreWindowBase
    {
        public RenderCoreWindow(RenderWindow _renderWindow) : base(_renderWindow)
        {
        }

        public override void DrawScene(RenderWindow _renderWindow)
        {
            _renderWindow.Clear(Color.Black);

            _renderWindow.Display();
        }
    }
}
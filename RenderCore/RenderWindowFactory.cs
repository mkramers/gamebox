using SFML.Graphics;
using SFML.Window;

namespace RenderCore
{
    public class RenderWindowFactory
    {
        public static RenderWindow CreateRenderWindow(string _name, uint _sizeX, uint _sizeY)
        {
            FloatRect viewPort = new FloatRect(0, 0, 1.0f, 1.0f);

            View view = new View(viewPort);

            VideoMode videoMode = new VideoMode(_sizeX, _sizeY);
            RenderWindow window = new RenderWindow(videoMode, _name);
            window.SetView(view);

            return window;
        } 
    }
}

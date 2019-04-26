using System;
using System.Linq.Expressions;
using System.Numerics;
using RenderCore;
using SFML.Graphics;
using SFML.System;

namespace GameBox
{
    internal class Program
    {
        private static void Main()
        {
            GameBox gameBox = new GameBox();
            gameBox.Run();
        }
    }

    public class GameBox
    {
        public void Run()
        {
            const string windowTitle = "GameBox";
            Vector2u windowSize = new Vector2u(600, 600);
            const float tileSize = 0.2f;

            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(windowTitle, windowSize);

            RenderCoreWindow window = new RenderCoreWindow(renderWindow, tileSize) { EnableGrid = true };

            ManBodyFactory manBodyFactory = new ManBodyFactory();
            IBody manBody = manBodyFactory.GetManBody(tileSize);
            window.AddDrawable(manBody.GetDrawable());

            window.StartRenderLoop();
        }
    }
}

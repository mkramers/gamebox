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
            FloatRect viewRect = new FloatRect(0, -5, 20, 20);

            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(windowTitle, windowSize, viewRect);

            RenderCoreWindow window = new RenderCoreWindow(renderWindow) { EnableGrid = true };

            ManBodyFactory manBodyFactory = new ManBodyFactory();
            IBody manBody = manBodyFactory.GetManBody();
            window.AddDrawable(manBody.GetDrawable());

            window.StartRenderLoop();
        }
    }
}

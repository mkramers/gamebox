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
            const int tileSize = 30;

            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(windowTitle, windowSize);

            RenderCoreWindow window = new RenderCoreWindow(renderWindow);

            ManBodyFactory manBodyFactory = new ManBodyFactory();
            IBody manBody = manBodyFactory.GetManBody();
            window.AddDrawable(manBody.GetDrawable());

            int rows = (int)Math.Round((float)windowSize.X / tileSize);
            int columns = (int)Math.Round((float)windowSize.Y / tileSize);
            Drawable grid = DrawableFactory.GetGrid(rows, columns, Vector2.One, 0.005f, Vector2.Zero);
            window.AddDrawable(grid);

            window.StartRenderLoop();
        }
    }
}

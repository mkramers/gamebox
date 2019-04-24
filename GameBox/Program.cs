using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenderCore;
using SFML.Graphics;
using SFML.Window;

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
            const int sizeX = 600;
            const int sizeY = 600;

            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(windowTitle, sizeX, sizeY);
            
            RenderCoreWindow window = new RenderCoreWindow(renderWindow);

            ManBodyFactory manBodyFactory = new ManBodyFactory();
            IBody manBody = manBodyFactory.GetManBody();

            window.AddBodyRepresentation(manBody.GetBodyRepresentation());

            window.StartRenderLoop();
        }
    }
}

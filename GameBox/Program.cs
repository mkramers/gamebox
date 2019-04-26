using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Windows.Input;
using RenderCore;
using SFML.Graphics;
using SFML.System;
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
            Vector2u windowSize = new Vector2u(600, 600);
            FloatRect viewRect = new FloatRect(0, -5, 20, 20);

            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(windowTitle, windowSize, viewRect);
            RenderCoreWindow window = new RenderCoreWindow(renderWindow) { EnableGrid = true };

            ManBodyFactory manBodyFactory = new ManBodyFactory();
            Sprite man = manBodyFactory.GetMan();
            window.AddDrawable(man);

            Dictionary<Keyboard.Key, ICommand> moveCommands = KeyCommandsFactory.GetTransformableMoveCommands(man, 1.0f);
            KeyCommandExecuter keyCommandExecuter = new KeyCommandExecuter(moveCommands);
            window.AddKeyHandler(keyCommandExecuter);

            window.StartRenderLoop();
        }
    }
}

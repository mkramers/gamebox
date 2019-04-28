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

            PhysicsController physics = new PhysicsController();
            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(windowTitle, windowSize, viewRect);
            RenderCoreWindow window = new RenderCoreWindow(renderWindow, physics) { EnableGrid = true };

            const float mass = 1.0f;
            ManBodyFactory manBodyFactory = new ManBodyFactory();
            BodySprite man = manBodyFactory.GetMan(mass);
            window.AddDrawable(man);

            physics.Add(man);

            Dictionary<Keyboard.Key, ICommand> moveCommands = KeyCommandsFactory.GetBodySpriteCommands(man, 1.0f);
            KeyCommandExecuter moveExecutor = new KeyCommandExecuter(moveCommands);
            window.AddKeyHandler(moveExecutor);

            ObjectFramework objectFramework = new ObjectFramework();
            objectFramework.Add(window);

            while (true)
            {
                objectFramework.Tick();
            }
        }
    }
}

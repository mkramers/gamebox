using System.Collections.Generic;
using System.Windows.Input;
using RenderCore;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameBox
{
    public class GameBox
    {
        public void Run()
        {
            const string windowTitle = "GameBox";
            Vector2u windowSize = new Vector2u(600, 600);
            FloatRect viewRect = new FloatRect(0, -5, 20, 20);

            PhysicsController physics = new PhysicsController();
            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(windowTitle, windowSize, viewRect);
            RenderCoreWindow window = new RenderCoreWindow(renderWindow, physics) {EnableGrid = true};
            
            ObjectFramework objectFramework = new ObjectFramework();
            objectFramework.Add(window);

            CreateMainCharacter(window, physics);

            while (true)
            {
                objectFramework.Tick();
            }
        }

        private static void CreateMainCharacter(RenderCoreWindow _window, PhysicsController _physics)
        {
            const float mass = 1.0f;
            ManBodyFactory manBodyFactory = new ManBodyFactory();
            BodySprite man = manBodyFactory.GetMan(mass);

            Dictionary<Keyboard.Key, ICommand> moveCommands = KeyCommandsFactory.GetBodySpriteCommands(man, 1.0f);
            KeyCommandExecuter moveExecutor = new KeyCommandExecuter(moveCommands);
            _window.AddKeyHandler(moveExecutor);

            _window.AddDrawable(man);
            _physics.Add(man);
        }
    }
}
using System.Collections.Generic;
using System.Numerics;
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
            FloatRect viewRect = new FloatRect(-10, 10, 20, 20);

            PhysicsController physics = new PhysicsController();
            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(windowTitle, windowSize, viewRect);
            GridWidget gridWidget = new GridWidget(renderWindow.GetView()) {IsDrawEnabled = true};

            RenderCoreWindow window = new RenderCoreWindow(renderWindow, new[] { gridWidget });

            TickableContainer tickableContainer = new TickableContainer();

            //order matters
            tickableContainer.Add(physics);
            tickableContainer.Add(window);

            CreateMainCharacter(window, physics);

            while (true)
            {
                tickableContainer.Tick();
            }
        }

        private static void CreateMainCharacter(RenderCoreWindow _window, PhysicsController _physics)
        {
            const float mass = 1.0f;
            ManBodyFactory manBodyFactory = new ManBodyFactory();
            BodySprite man = manBodyFactory.GetMan(mass);

            Dictionary<Keyboard.Key, ICommand> moveCommands = KeyCommandsFactory.GetBodySpriteCommands(man, 1.0f);
            KeyCommandExecuter moveExecutor = new KeyCommandExecuter(moveCommands);

            _window.ClearKeyHandlers();
            _window.AddKeyHandler(moveExecutor);

            _physics.Add(man);
            _window.Add(man);
        }
    }
}
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Input;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore
{
    public class Game
    {
        private readonly TickableContainer m_tickableContainer;

        public Game(string _windowTitle, Vector2u _windowSize)
        {
            FloatRect viewRect = new FloatRect(-10, 10, 20, 20);

            PhysicsController physics = new PhysicsController();
            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(_windowTitle, _windowSize, viewRect);
            GridWidget gridWidget = new GridWidget(renderWindow.GetView()) { IsDrawEnabled = true };

            RenderCoreWindow window = new RenderCoreWindow(renderWindow, new[] { gridWidget });

            m_tickableContainer = new TickableContainer();

            CreateMainCharacter(window, physics);

            //order matters
            m_tickableContainer.Add(physics);
            m_tickableContainer.Add(window);
        }

        public void StartLoop()
        {
            while (true)
            {
                m_tickableContainer.Tick();
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

    public class TickableContainer
    {
        private readonly Stopwatch m_stopwatch;
        private readonly BlockingCollection<ITickable> m_tickables;

        public TickableContainer()
        {
            m_tickables = new BlockingCollection<ITickable>();

            m_stopwatch = new Stopwatch();
            m_stopwatch.Start();
        }

        public void Tick()
        {
            long elapsed = m_stopwatch.ElapsedMilliseconds;

            foreach (ITickable tickable in m_tickables)
            {
                tickable.Tick(elapsed);
            }

            m_stopwatch.Restart();
        }

        public void Add(ITickable _tickable)
        {
            m_tickables.Add(_tickable);
        }
    }
}
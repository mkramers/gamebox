using System;
using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using GameBox;
using RenderCore;
using SFML.System;
using SFML.Window;

namespace AetherBox
{
    public class AetherBox : Game
    {
        private readonly IEntity m_manEntity;
        private readonly ViewController m_viewController;

        public AetherBox(string _windowTitle, Vector2u _windowSize) : base(_windowTitle, _windowSize)
        {
            IPhysics physics = EntityPhysics;

            physics.SetGravity(Vector2.Zero);

            const float mass = 0.01f;
            const float force = 0.666f;

            m_manEntity = EntityFactory.CreateEntity(mass, Vector2.Zero, physics, ResourceId.MAN,
                BodyType.Dynamic);

            AddEntity(m_manEntity);
            
            Dictionary<Keyboard.Key, IKeyCommand>
                moveCommands = KeyCommandsFactory.GetMovementCommands(m_manEntity, force);
            KeyHandler moveExecutor = KeyHandlerFactory.CreateKeyHandler(moveCommands);

            AddKeyHandler(moveExecutor);

            m_viewController = new ViewController(new Vector2(20, 20));

            RenderCoreWindow.SetViewController(m_viewController);
        }

        public override void Tick(TimeSpan _elapsed)
        {
            Vector2 manPosition = m_manEntity.GetPosition();

            m_viewController.SetCenter(manPosition);
        }
    }
}
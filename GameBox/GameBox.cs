using System;
using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using RenderCore;
using SFML.System;
using SFML.Window;

namespace GameBox
{
    public class GameBox : Game
    {
        private IEntity m_manEntity;
        private ViewController m_viewController;

        public GameBox(string _windowTitle, Vector2u _windowSize) : base(_windowTitle, _windowSize)
        {
            CreateEntities();
        }


        private void CreateEntities()
        {
            IPhysics physics = GetPhysics();

            const float mass = 0.1f;
            m_manEntity = EntityFactory.CreateEntity(mass, -5 * Vector2.UnitY, physics, ResourceId.MAN,
                BodyType.Dynamic);

            AddEntity(m_manEntity);

            IEnumerable<IEntity> woodEntities = LandscapeFactory.CreateLandscape(physics);

            foreach (IEntity woodEntity in woodEntities)
            {
                AddEntity(woodEntity);
            }

            Dictionary<Keyboard.Key, IKeyCommand>
                moveCommands = KeyCommandsFactory.GetMovementCommands(m_manEntity, 2f);
            KeyHandler moveExecutor = KeyHandlerFactory.CreateKeyHandler(moveCommands);

            AddKeyHandler(moveExecutor);

            m_viewController = new ViewController(new Vector2(20, 20));

            RenderCoreWindow renderCoreWindow = GetRenderCoreWindow();
            renderCoreWindow.SetViewController(m_viewController);
        }

        public override void Tick(TimeSpan _elapsed)
        {
            Vector2 manPosition = m_manEntity.GetPosition();

            m_viewController.SetCenter(manPosition);
        }
    }
}
using System.Numerics;
using Aether.Physics2D.Dynamics;
using RenderCore;
using SFML.Graphics;
using SFML.System;

namespace GameBox
{
    public class GameBox : Game
    {
        public GameBox(string _windowTitle, Vector2u _windowSize) : base(_windowTitle, _windowSize)
        {
            IPhysics physics = Physics;

            const float mass = 0.1f;
            const float force = 0.666f;

            IEntity manEntity = EntityFactory.CreateEntity(mass, 2 * Vector2.One, physics, ResourceId.MAN,
                BodyType.Dynamic);

            AddEntity(manEntity);

            SampleMap map = new SampleMap();
            AddMap(map, physics);

            KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(manEntity, force);

            KeyHandlers.Add(moveExecutor);

            EntityCenterFollowerViewController viewController = new EntityCenterFollowerViewController(0.03f, manEntity)
            {
                Center = new Vector2f(0, 0),
                Size = new Vector2f(20, 20),
            };

            RenderCoreWindow.SetViewProvider(viewController);
        }
    }
}
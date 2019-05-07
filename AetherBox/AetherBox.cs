using System.Numerics;
using Aether.Physics2D.Dynamics;
using RenderCore;
using SFML.System;

namespace AetherBox
{
    public class AetherBox : Game
    {
        public AetherBox(string _windowTitle, Vector2u _windowSize) : base(_windowTitle, _windowSize)
        {
            IPhysics physics = Physics;

            physics.SetGravity(Vector2.Zero);

            const float mass = 0.01f;
            const float force = 0.666f;

            IEntity manEntity = EntityFactory.CreateEntity(mass, Vector2.Zero, physics, ResourceId.MAN,
                BodyType.Dynamic);

            AddEntity(manEntity);

            KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(manEntity, force);

            AddKeyHandler(moveExecutor);

            ViewController viewController = new EntityCenterFollowerViewController(new Vector2(20, 20), manEntity);

            RenderCoreWindow.SetViewController(viewController);
        }
    }
}
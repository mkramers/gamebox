using System.Numerics;
using Aether.Physics2D.Dynamics;
using RenderCore;
using SFML.Graphics;
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

            LineSegment lineSegment = new LineSegment(new Vector2(-5, 5), new Vector2(5, 5));

            IBody edgeBody = Physics.CreateEdge(lineSegment);
            ShapeDrawable edgeDrawable = DrawableFactory.GetLineSegment(lineSegment, 0.2f);
            Entity edgeEntity = new Entity(edgeDrawable, edgeBody);
            AddEntity(edgeEntity);

            MultiDrawable sample = SampleFactory.GetSample();
            sample.SetRenderPosition(Vector2.Zero);
            RenderCoreWindow.Add(sample);

            KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(manEntity, force);

            KeyHandlers.Add(moveExecutor);

            View view = new View(Vector2.Zero.GetVector2F(), new Vector2(20, 20).GetVector2F());
            ViewControllerBase viewController = new EntityCenterFollowerViewController(view, 0.03f, manEntity);

            RenderCoreWindow.SetViewController(viewController);
        }
    }
}
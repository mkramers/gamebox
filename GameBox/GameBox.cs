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
            
            SampleMap map = new SampleMap();

            KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(manEntity, force);

            KeyHandlers.Add(moveExecutor);

            View view = new View(new Vector2f(0, 0), new Vector2f(30, 30));
            EntityFollowerViewProvider
                viewProvider = new EntityFollowerViewProvider(0.03f, manEntity, view);

            AddWidget(viewProvider);

            IRenderCoreTarget scene = RenderCoreWindow.GetScene();
            scene.SetViewProvider(viewProvider);

            GridWidget gridWidget = new GridWidget(viewProvider);
            scene.AddDrawable(gridWidget);
            
            AddWidget(gridWidget);

            AddMap(map, physics);

            AddEntity(manEntity);
        }
    }
}
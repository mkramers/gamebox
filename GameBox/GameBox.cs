using System.Numerics;
using Aether.Physics2D.Dynamics;
using RenderCore;
using SFML.Graphics;
using SFML.System;

namespace GameBox
{
    public class GameBox : Game
    {
        public GameBox(string _windowTitle, Vector2u _windowSize, Vector2 _gravity) : base(_windowTitle, _windowSize,
            _gravity)
        {
            IPhysics physics = Physics;
            physics.SetGravity(new Vector2(0, 0));
            
            IEntity manEntity = CreateMan(physics);
            
            View view = new View(new Vector2f(0, 0), new Vector2f(30, 30));
            EntityFollowerViewProvider
                viewProvider = new EntityFollowerViewProvider(manEntity, view);
            
            IRenderCoreTarget scene = RenderCoreWindow.GetScene();

            SetSceneViewerProvider(scene, viewProvider);

            AddWidgets(scene, viewProvider);
            
            AddMap(physics);

            AddMan(manEntity);
        }

        private void AddMan(IEntity _manEntity)
        {
            AddManKeyHandler(_manEntity);

            AddEntity(_manEntity);
        }

        private static void SetSceneViewerProvider(IRenderCoreTarget _scene, EntityFollowerViewProvider _viewProvider)
        {
            _scene.SetViewProvider(_viewProvider);
        }

        private void AddMap(IPhysics _physics)
        {
            SampleMap map = new SampleMap();
            AddMap(map, _physics);
        }

        private void AddWidgets(IRenderObjectContainer _scene, EntityFollowerViewProvider _viewProvider)
        {
            AddWidget(_viewProvider);

            GridWidget gridWidget = new GridWidget(_viewProvider);
            _scene.AddDrawable(gridWidget);

            AddWidget(gridWidget);

            MultiDrawable<RectangleShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One, 0.2f);
            _scene.AddDrawable(crossHairs);
        }

        private void AddManKeyHandler(IEntity _manEntity)
        {
            const float force = 0.666f;

            KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(_manEntity, force);

            KeyHandlers.Add(moveExecutor);
        }

        private static IEntity CreateMan(IPhysics _physics)
        {
            const float mass = 0.1f;

            Vector2 manPosition = new Vector2(0, -3);

            Sprite sprite = SpriteFactory.GetSprite(ResourceId.MAN);
            IEntity manEntity =
                SpriteEntityFactory.CreateSpriteEntity(mass, manPosition, _physics, BodyType.Dynamic, sprite);
            return manEntity;
        }
    }
}
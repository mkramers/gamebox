using System.Numerics;
using Aether.Physics2D.Dynamics;
using RenderCore;
using SFML.Graphics;
using SFML.System;

namespace GameBox
{
    public class GameBox : Game
    {
        public GameBox(string _windowTitle, Vector2u _windowSize, Vector2 _gravity, float _aspectRatio) : base(_windowTitle, _windowSize,
            _gravity, _aspectRatio)
        {
            IPhysics physics = Physics;
            physics.SetGravity(new Vector2(0, 30));

            IEntity manEntity = CreateMan(physics);

            View view = new View(new Vector2f(0, -100), new Vector2f(300, 300));
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

        private static void SetSceneViewerProvider(IRenderCoreTarget _scene, IViewProvider _viewProvider)
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

            GridWidget gridWidget = new GridWidget(_viewProvider, 0.5f, new Vector2(10, 10));
            _scene.AddDrawable(gridWidget);

            AddWidget(gridWidget);

            MultiDrawable<RectangleShape> crossHairs = DrawableFactory.GetCrossHair(50 * Vector2.One, 2f);
            _scene.AddDrawable(crossHairs);

            AddFpsWidget();
        }

        private void AddManKeyHandler(IEntity _manEntity)
        {
            const float force = 66.6f;

            KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(_manEntity, force);

            KeyHandlers.Add(moveExecutor);
        }

        private static IEntity CreateMan(IPhysics _physics)
        {
            const float mass = 0.001f;

            Vector2 manPosition = new Vector2(0, -100);
            Vector2 manScale = new Vector2(30f, 30f);

            Texture texture = ResourceFactory.GetTexture(ResourceId.MK);

            Sprite sprite = new Sprite(texture)
            {
                Scale = new Vector2f(manScale.X / texture.Size.X, manScale.Y / texture.Size.Y)
            };

            IEntity manEntity =
                SpriteEntityFactory.CreateSpriteEntity(mass, manPosition, manScale, _physics, BodyType.Dynamic, sprite);
            return manEntity;
        }

        private void AddFpsWidget()
        {
            const float fontScale = 0.04f;
            const uint fontSize = 72;
            Vector2 textPosition = new Vector2(fontScale, 1.0f - 1.5f * fontScale);

            FontFactory fontFactory = new FontFactory();
            Font font = fontFactory.GetFont(FontId.ROBOTO);

            Text textRenderObject = new Text("", font, fontSize)
            {
                Scale = new Vector2f(fontScale / fontSize, fontScale / fontSize),
                FillColor = Color.Blue
            };
            FpsTextWidget fpsTextWidget = new FpsTextWidget(5, textRenderObject);
            fpsTextWidget.SetPosition(textPosition);

            IRenderCoreTarget overlay = RenderCoreWindow.GetOverlay();
            overlay.AddDrawable(fpsTextWidget);

            AddWidget(fpsTextWidget);
        }
    }
}
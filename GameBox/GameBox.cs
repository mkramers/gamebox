using System.Numerics;
using Aether.Physics2D.Dynamics;
using GameCore;
using GameCore.Entity;
using GameCore.Input.Key;
using GameCore.Maps;
using GameCore.ViewProvider;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Render;
using RenderCore.Resource;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using SFML.Graphics;
using SFML.System;

namespace GameBox
{
    public class GameBox : Game
    {
        public GameBox(string _windowTitle, Vector2u _windowSize, Vector2 _gravity, float _aspectRatio) : base(
            _windowTitle, _windowSize,
            _gravity, _aspectRatio)
        {
            IPhysics physics = Physics;
            physics.SetGravity(new Vector2(0, 3));

            IEntity manEntity = CreateMan(physics);

            View view = new View(new Vector2f(0, -6.5f), new Vector2f(50, 50));
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
            const string mapFilePath = @"C:\dev\GameBox\RenderCore\Resources\art\sample_tree_map.json";

            SampleMap2 map = new SampleMap2(mapFilePath, _physics);
            AddMap(map, _physics);

            IRenderCoreTarget scene = RenderCoreWindow.GetScene();
            scene.AddDrawable(map.LineDrawable);
        }

        private void AddWidgets(IRenderObjectContainer _scene, EntityFollowerViewProvider _viewProvider)
        {
            AddWidget(_viewProvider);

            GridWidget gridWidget = new GridWidget(_viewProvider, 0.1f, new Vector2(1, 1));
            _scene.AddDrawable(gridWidget);

            AddWidget(gridWidget);

            MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One, 0.05f);
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
            const float mass = 0.1f;

            Vector2 manPosition = new Vector2(0, -10);
            Vector2 manScale = new Vector2(2f, 2f);

            Texture texture = ResourceFactory.GetTexture(ResourceId.MK);

            Sprite sprite = new Sprite(texture)
            {
                Scale = new Vector2f(manScale.X / texture.Size.X, manScale.Y / texture.Size.Y)
            };

            IEntity manEntity =
                SpriteEntityFactory.CreateSpriteEntity(mass, manPosition, _physics, BodyType.Dynamic, sprite);
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
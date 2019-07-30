using System;
using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using GameCore;
using GameCore.Entity;
using GameCore.Input.Key;
using GameCore.Maps;
using GameCore.ViewProvider;
using LibExtensions;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Render;
using RenderCore.Resource;
using RenderCore.Widget;
using SFML.Graphics;
using SFML.System;

namespace GameBox
{
    public class GameRunnerBox : IDisposable
    {
        public void Dispose()
        {
            m_gameRunner?.Dispose();
        }

        private readonly GameRunner m_gameRunner;

        public GameRunnerBox(string _windowTitle, Vector2u _windowSize, Vector2 _gravity, float _aspectRatio)
        {
            m_gameRunner = new GameRunner(_windowTitle, _windowSize, Vector2.Zero, _aspectRatio);

            IPhysics physics = m_gameRunner.GetPhysics();
            physics.SetGravity(new Vector2(0, 5.5f));

            IEntity manEntity = CreateMan(physics);

            View view = new View(new Vector2f(0, -6.5f), new Vector2f(35, 35));
            EntityFollowerViewProvider
                viewProvider = new EntityFollowerViewProvider(manEntity, view);

            m_gameRunner.SetSceneViewProvider(viewProvider);

            AddWidgets(viewProvider);

            AddMap(physics);

            AddMan(manEntity);

            //temp
            CoinThing c = new CoinThing(physics);
            foreach (IEntity coinEntity in c.CoinEntities)
            {
                m_gameRunner.AddEntity(coinEntity);
            }
        }


        private void AddMan(IEntity _manEntity)
        {
            AddManKeyHandler(_manEntity);

            m_gameRunner.AddEntity(_manEntity);
        }

        private void AddMap(IPhysics _physics)
        {
            const string mapFilePath = @"C:\dev\GameBox\RenderCore\Resources\art\sample_tree_map.json";

            SampleMap2 map = new SampleMap2(mapFilePath, _physics);

            foreach (IEntity woodEntity in map.GetEntities(_physics))
            {
                m_gameRunner.AddEntity(woodEntity);
            }

            IEnumerable<IDrawable> mapDrawables = map.GetDrawables();
            foreach (IDrawable mapDrawable in mapDrawables)
            {
                m_gameRunner.AddDrawableToScene(mapDrawable);
            }
        }

        private void AddWidgets(EntityFollowerViewProvider _viewProvider)
        {
            m_gameRunner.AddWidget(_viewProvider);

            WidgetFontSettings widgetFontSettings = new WidgetFontSettings();
            FontSettings gridLabelFontSettings = widgetFontSettings.GetSettings(WidgetFontSettingsType.LABELED_GRID);
            LabeledGridWidget gridWidget =
                new LabeledGridWidget(_viewProvider, 0.1f, new Vector2(1, 1), gridLabelFontSettings);
            //_scene.AddDrawable(gridWidget);

            //AddWidget(gridWidget);

            MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One, 0.05f);
            m_gameRunner.AddDrawableToScene(crossHairs);

            m_gameRunner.AddFpsWidget();
        }

        private void AddManKeyHandler(IEntity _manEntity)
        {
            const float force = 26.6f;

            KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(_manEntity, force);

            m_gameRunner.AddKeyHandler(moveExecutor);
        }

        private static IEntity CreateMan(IPhysics _physics)
        {
            const float mass = 0.1f;

            Vector2 manPosition = new Vector2(0, -10);
            Vector2 manScale = new Vector2(2f, 2f);

            Texture texture = ResourceFactory.GetTexture(ResourceId.MK);

            Vector2f spriteScale = new Vector2f(manScale.X / texture.Size.X, manScale.Y / texture.Size.Y);
            Sprite sprite = new Sprite(texture)
            {
                Scale = spriteScale
            };

            IEntity manEntity =
                SpriteEntityFactory.CreateSpriteEntity(mass, manPosition, _physics, BodyType.Dynamic, sprite);
            manEntity.Collided += ManEntity_Collided;
            return manEntity;
        }

        private static bool ManEntity_Collided(Fixture _sender, Fixture _other, Aether.Physics2D.Dynamics.Contacts.Contact _contact)
        {
            return true;
        }
        
        public void StartLoop()
        {
            m_gameRunner.StartLoop();
        }
    }
}
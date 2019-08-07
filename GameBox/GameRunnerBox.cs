using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Aether.Physics2D.Dynamics.Contacts;
using Common.Cache;
using Common.Caches;
using GameCore;
using GameCore.Entity;
using GameCore.Input.Key;
using GameCore.Maps;
using GameCore.ViewProvider;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Widget;
using ResourceUtilities.Aseprite;
using SFML.Graphics;
using SFML.System;
using TGUI;

namespace GameBox
{
    public class GameRunnerBox : IDisposable
    {
        private readonly GameRunner m_gameRunner;
        private readonly CoinThing m_coinThing;

        public GameRunnerBox(string _windowTitle, Vector2u _windowSize, Vector2 _gravity, float _aspectRatio)
        {
            m_gameRunner = new GameRunner(_windowTitle, _windowSize, Vector2.Zero, _aspectRatio);

            IPhysics physics = m_gameRunner.GetPhysics();
            physics.SetGravity(new Vector2(0, 5.5f));

            SpriteResourceLoader loader = new SpriteResourceLoader();
            loader.LoadResources(@".\");

            //create man
            IEntity manEntity;
            {
                const float mass = 0.1f;

                Vector2 manPosition = new Vector2(0, -10);
                Vector2 manScale = new Vector2(2f, 2f);

                const string mkFilePath = @"C:\dev\GameBox\RenderCore\Resources\art\mk.png";
                Texture texture = new Texture(mkFilePath);

                Vector2f spriteScale = new Vector2f(manScale.X / texture.Size.X, manScale.Y / texture.Size.Y);
                Sprite sprite = new Sprite(texture)
                {
                    Scale = spriteScale
                };

                manEntity = SpriteEntityFactory.CreateSpriteEntity(mass, manPosition, physics, BodyType.Dynamic, sprite);
            }

            View view = new View(new Vector2f(0, -6.5f), new Vector2f(35, 35));
            EntityFollowerViewProvider
                viewProvider = new EntityFollowerViewProvider(manEntity, view);

            m_gameRunner.SetSceneViewProvider(viewProvider);

            //widgets
            {
                m_gameRunner.AddWidget(viewProvider);

                WidgetFontSettings widgetFontSettings = new WidgetFontSettings();
                FontSettings gridLabelFontSettings = widgetFontSettings.GetSettings(WidgetFontSettingsType.LABELED_GRID);
                LabeledGridWidget gridWidget =
                    new LabeledGridWidget(viewProvider, 0.1f, new Vector2(1, 1), gridLabelFontSettings);
                
                //m_gameRunner.AddDrawableToScene(gridWidget);
                //m_gameRunner.AddWidget(gridWidget);

                MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One, 0.05f);
                m_gameRunner.AddDrawableToScene(crossHairs);

                m_gameRunner.AddFpsWidget();
            }

            //add map
            {
                const string mapFilePath = @"C:\dev\GameBox\RenderCore\Resources\art\sample_tree_map.json";

                SampleMap2 map = new SampleMap2(mapFilePath, physics);

                foreach (IEntity woodEntity in map.GetEntities(physics))
                {
                    m_gameRunner.AddEntity(woodEntity);
                }

                IEnumerable<IDrawable> mapDrawables = map.GetDrawables();
                foreach (IDrawable mapDrawable in mapDrawables)
                {
                    m_gameRunner.AddDrawableToScene(mapDrawable);
                }
            }

            //key handler
            {
                const float force = 26.6f;

                KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(manEntity, force);

                m_gameRunner.AddKeyHandler(moveExecutor);
            }

            m_gameRunner.AddEntity(manEntity);

            //temp
            List<Coin> coins = CoinEntitiesFactory.GetCoins(physics).ToList();

            m_coinThing = new CoinThing(manEntity, coins, m_gameRunner.GetScene(), m_gameRunner.GetGui());
            m_gameRunner.AddGameModule(m_coinThing);
        }

        public void Dispose()
        {
            m_gameRunner?.Dispose();
        }
        
        public void StartLoop()
        {
            m_gameRunner.StartLoop();
        }
    }
}
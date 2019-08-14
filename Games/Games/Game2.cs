using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Grid;
using Common.Tickable;
using GameCore;
using GameCore.Entity;
using GameCore.Input.Key;
using GameCore.Maps;
using GameCore.ViewProvider;
using GameResources.Attributes;
using GameResources.Converters;
using Games.Coins;
using Games.Maps;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Resource;
using RenderCore.ViewProvider;
using ResourceUtilities.Aseprite;
using SFML.Graphics;
using SFML.System;
using TGUI;

namespace Games.Games
{
    public class Game2 : IGameProvider, IViewProvider
    {
        private readonly List<IDrawable> m_drawables;
        private readonly List<ITickable> m_tickables;
        private readonly List<IGameProvider> m_gameProviders;
        private readonly IViewProvider m_viewProvider;

        public Game2(IPhysics _physics, Gui _gui)
        {
            m_drawables = new List<IDrawable>();
            m_tickables = new List<ITickable>();
            m_gameProviders = new List<IGameProvider>();
            
            _physics.SetGravity(new Vector2(0, 5.5f));

            const string resourceRootDirectory = @"C:\dev\GameBox\Resources\sprite";
            ResourceManager<SpriteResources> manager = new ResourceManager<SpriteResources>(resourceRootDirectory);

            //create man
            IEntity manEntity;
            {
                const float mass = 0.1f;

                Vector2 manPosition = new Vector2(0, -10);
                Vector2 manScale = new Vector2(2f, 2f);

                Resource<Texture> resource = manager.GetTextureResource(SpriteResources.OBJECT_MKRAMERS_LAYER);
                Texture texture = resource.Load();

                Vector2f spriteScale = new Vector2f(manScale.X / texture.Size.X, manScale.Y / texture.Size.Y);
                Sprite sprite = new Sprite(texture)
                {
                    Scale = spriteScale
                };

                manEntity = SpriteEntityFactory.CreateSpriteEntity(mass, manPosition, _physics, BodyType.Dynamic,
                    sprite);

                m_drawables.Add(manEntity);
                m_tickables.Add(manEntity);
            }

            View view = new View(new Vector2f(0, -6.5f), new Vector2f(35, 35));
            EntityFollowerViewProvider viewProvider = new EntityFollowerViewProvider(manEntity, view);

            m_viewProvider = viewProvider;

            //widgets
            {
                m_tickables.Add(viewProvider);

                //WidgetFontSettings widgetFontSettings = new WidgetFontSettings();
                //FontSettings gridLabelFontSettings =
                //    widgetFontSettings.GetSettings(WidgetFontSettingsType.LABELED_GRID);

                //LabeledGridWidget gridWidget = new LabeledGridWidget(viewProvider, 0.1f, new Vector2(1, 1), gridLabelFontSettings);
                //m_gameRunner.AddDrawableToScene(gridWidget);
                //m_gameRunner.AddWidget(gridWidget);

                MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One);
                m_drawables.Add(crossHairs);
            }

            //add map
            {
                Resource<Texture> mapSceneResource = manager.GetTextureResource(SpriteResources.MAP_TREE_SCENE);
                Texture mapSceneTexture = mapSceneResource.Load();

                Resource<Bitmap> mapCollisionResource = manager.GetBitmapResource(SpriteResources.MAP_TREE_COLLISION);
                Bitmap mapCollisionBitmap = mapCollisionResource.Load();

                Grid<ComparableColor> mapCollisionGrid =
                    BitmapToGridConverter.GetColorGridFromBitmap(mapCollisionBitmap);

                IMap map = new SampleMap2(mapSceneTexture, mapCollisionGrid, _physics);

                foreach (IEntity mapEntity in map.GetEntities(_physics))
                {
                    m_drawables.Add(mapEntity);
                    m_tickables.Add(mapEntity);
                }

                IEnumerable<IDrawable> mapDrawables = map.GetDrawables();
                foreach (IDrawable mapDrawable in mapDrawables)
                {
                    m_drawables.Add(mapDrawable);
                }
            }

            //key handler
            {
                const float force = 26.6f;

                KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(manEntity, force);

                m_tickables.Add(moveExecutor);
            }

            //temp
            List<Coin> coins = CoinEntitiesFactory.GetCoins(resourceRootDirectory, _physics).ToList();

            CoinThing coinThing = new CoinThing(manEntity, coins, _gui);
            coinThing.PauseGame += (_sender, _e) => PauseGame?.Invoke(_sender, _e);
            coinThing.ResumeGame += (_sender, _e) => ResumeGame?.Invoke(_sender, _e);

            m_gameProviders.Add(coinThing);
        }
        
        public IEnumerable<IDrawable> GetDrawables()
        {
            List<IDrawable> drawables = new List<IDrawable>();
            drawables.AddRange(m_drawables);

            IEnumerable<IDrawable> subGameDrawables = m_gameProviders.SelectMany(_gameProvider => _gameProvider.GetDrawables());
            drawables.AddRange(subGameDrawables);

            return drawables;
        }

        public IEnumerable<ITickable> GetTickables()
        {
            List<ITickable> tickables = new List<ITickable>();
            tickables.AddRange(m_tickables);

            IEnumerable<ITickable> subGameTickables = m_gameProviders.SelectMany(_gameProvider => _gameProvider.GetTickables());
            tickables.AddRange(subGameTickables);

            return tickables;
        }

        public View GetView()
        {
            return m_viewProvider.GetView();
        }

        public void Dispose()
        {
        }

        public event EventHandler PauseGame;
        public event EventHandler ResumeGame;
    }
}
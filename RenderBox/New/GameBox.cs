using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Grid;
using Common.Tickable;
using GameBox;
using GameCore;
using GameCore.Entity;
using GameCore.Input.Key;
using GameCore.Maps;
using GameCore.ViewProvider;
using GameResources.Attributes;
using GameResources.Converters;
using LibExtensions;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Resource;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using ResourceUtilities.Aseprite;
using SFML.Graphics;
using SFML.System;
using TGUI;

namespace RenderBox.New
{
    public class DrawableProvider : IDrawableProvider
    {
        private readonly IDrawable m_drawable;

        public DrawableProvider(IDrawable _drawable)
        {
            m_drawable = _drawable;
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return new[] { m_drawable };
        }
    }

    public class Game3 : IDisposable
    {
        private readonly GameBox m_gameBox;

        public Game3()
        {
            m_gameBox = new GameBox();

            const float size = 25;
            Vector2 sceneSize = new Vector2(size, size);
            Vector2 scenePosition = -Vector2.One;
            FloatRect viewRect = new FloatRect(scenePosition.GetVector2F(), sceneSize.GetVector2F());
            View view = new View(viewRect);

            ViewProviderBase viewProvider = new ViewProviderBase(view);
            m_gameBox.SetViewProvider(viewProvider);

            //MultiDrawable<RectangleShape> box = DrawableFactory.GetBox(sceneSize, 1);
            //scene.AddDrawable(box);

            WidgetFontSettings widgetFontSettings = new WidgetFontSettings();
            FontSettings gridLabelFontSettings = widgetFontSettings.GetSettings(WidgetFontSettingsType.LABELED_GRID);
            LabeledGridWidget gridWidget = new LabeledGridWidget(viewProvider, 0.5f * Vector2.One, gridLabelFontSettings);

            m_gameBox.AddDrawable(gridWidget);

            MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One);
            m_gameBox.AddDrawable(crossHairs);

            m_gameBox.AddTickable(gridWidget);

            m_gameBox.AddFpsWidget();

            ResourceManager<SpriteResources> manager =
                new ResourceManager<SpriteResources>(@"C:\dev\GameBox\Resources\sprite");

            Resource<Texture> mapSceneResource = manager.GetTextureResource(SpriteResources.MAP_TREE_SCENE);
            Texture mapSceneTexture = mapSceneResource.Load();

            Resource<Bitmap> mapCollisionResource = manager.GetBitmapResource(SpriteResources.MAP_TREE_COLLISION);
            Bitmap mapCollisionBitmap = mapCollisionResource.Load();

            Grid<ComparableColor> mapCollisionGrid = BitmapToGridConverter.GetColorGridFromBitmap(mapCollisionBitmap);

            IMap map = new SampleMap2(mapSceneTexture, mapCollisionGrid, m_gameBox.GetPhysics());

            IEnumerable<IDrawable> mapDrawables = map.GetDrawables();
            foreach (IDrawable mapDrawable in mapDrawables)
            {
                m_gameBox.AddDrawable(mapDrawable);
            }
        }

        public void Dispose()
        {
            m_gameBox.Dispose();
        }

        public void StartLoop()
        {
            m_gameBox.StartLoop();
        }
    }

    public class Game2 : IDisposable
    {
        private readonly GameBox m_gameBox;

        public Game2()
        {
            m_gameBox = new GameBox();

            IPhysics physics = m_gameBox.GetPhysics();
            physics.SetGravity(new Vector2(0, 5.5f));

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

                manEntity = SpriteEntityFactory.CreateSpriteEntity(mass, manPosition, physics, BodyType.Dynamic,
                    sprite);
            }

            View view = new View(new Vector2f(0, -6.5f), new Vector2f(35, 35));
            EntityFollowerViewProvider
                viewProvider = new EntityFollowerViewProvider(manEntity, view);

            m_gameBox.SetViewProvider(viewProvider);

            //widgets
            {
                m_gameBox.AddTickable(viewProvider);

                //WidgetFontSettings widgetFontSettings = new WidgetFontSettings();
                //FontSettings gridLabelFontSettings =
                //    widgetFontSettings.GetSettings(WidgetFontSettingsType.LABELED_GRID);

                //LabeledGridWidget gridWidget = new LabeledGridWidget(viewProvider, 0.1f, new Vector2(1, 1), gridLabelFontSettings);
                //m_gameRunner.AddDrawableToScene(gridWidget);
                //m_gameRunner.AddWidget(gridWidget);

                MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One);
                m_gameBox.AddDrawable(crossHairs);

                m_gameBox.AddFpsWidget();
            }

            //add map
            {
                Resource<Texture> mapSceneResource = manager.GetTextureResource(SpriteResources.MAP_TREE_SCENE);
                Texture mapSceneTexture = mapSceneResource.Load();

                Resource<Bitmap> mapCollisionResource = manager.GetBitmapResource(SpriteResources.MAP_TREE_COLLISION);
                Bitmap mapCollisionBitmap = mapCollisionResource.Load();

                Grid<ComparableColor> mapCollisionGrid =
                    BitmapToGridConverter.GetColorGridFromBitmap(mapCollisionBitmap);

                IMap map = new SampleMap2(mapSceneTexture, mapCollisionGrid, physics);

                foreach (IEntity mapEntity in map.GetEntities(physics))
                {
                    m_gameBox.AddTickable(mapEntity);
                    m_gameBox.AddDrawable(mapEntity);
                }

                IEnumerable<IDrawable> mapDrawables = map.GetDrawables();
                foreach (IDrawable mapDrawable in mapDrawables)
                {
                    m_gameBox.AddDrawable(mapDrawable);
                }
            }

            //key handler
            {
                const float force = 26.6f;

                KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(manEntity, force);

                m_gameBox.AddTickable(moveExecutor);
            }

            m_gameBox.AddTickable(manEntity);
            m_gameBox.AddDrawable(manEntity);

            //temp
            List<Coin> coins = CoinEntitiesFactory.GetCoins(resourceRootDirectory, physics).ToList();

            CoinThing coinThing = new CoinThing(manEntity, coins, m_gameBox.GetGui());
            coinThing.PauseGame += (_sender, _e) => m_gameBox.SetIsPaused(true);
            coinThing.ResumeGame += (_sender, _e) => m_gameBox.SetIsPaused(false);

            m_gameBox.AddDrawableProvider(coinThing);
            m_gameBox.AddTickable(coinThing);
        }

        public void Dispose()
        {
            m_gameBox.Dispose();
        }

        public void StartLoop()
        {
            m_gameBox.StartLoop();
        }
    }

    public class GameBox : IGameBox
    {
        private readonly IGameBox m_gameBox;
        private readonly IPhysics m_physics;

        public GameBox()
        {
            m_gameBox = new GameBoxCore();

            m_physics = new Physics(new Vector2(0, 5.5f));
            AddTickable(m_physics);
        }

        public void StartLoop()
        {
            m_gameBox.StartLoop();
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_gameBox.SetViewProvider(_viewProvider);
        }

        public void SetIsPaused(bool _isPaused)
        {
            m_gameBox.SetIsPaused(_isPaused);
        }
        
        public Gui GetGui()
        {
            return m_gameBox.GetGui();
        }

        public Vector2u GetWindowSize()
        {
            return m_gameBox.GetWindowSize();
        }

        public void AddDrawableProvider(IDrawableProvider _drawableProvider)
        {
            m_gameBox.AddDrawableProvider(_drawableProvider);
        }

        public void AddTickable(ITickable _tickable)
        {
            m_gameBox.AddTickable(_tickable);
        }
        
        public IPhysics GetPhysics()
        {
            return m_physics;
        }

        public void Dispose()
        {
            m_gameBox.Dispose();
            m_physics.Dispose();
        }
    }

    public static class GameBoxExtensions
    {
        public static void AddDrawable(this IGameBox _gameBox, IDrawable _drawable)
        {
            DrawableProvider drawableProvider = new DrawableProvider(_drawable);
            _gameBox.AddDrawableProvider(drawableProvider);
        }

        public static void AddFpsWidget(this IGameBox _gameBox)
        {
            WidgetFontSettings widgetFontSettingsFactory = new WidgetFontSettings();
            FontSettings fpsFontSettings = widgetFontSettingsFactory.GetSettings(WidgetFontSettingsType.FPS_COUNTER);

            Vector2u windowSize = _gameBox.GetWindowSize();

            Vector2 textPosition = new Vector2(windowSize.X / 2.0f, windowSize.Y - fpsFontSettings.Size);

            FpsLabel fpsWidget = new FpsLabel(5, fpsFontSettings)
            {
                Position = textPosition.GetVector2F()
            };

            Gui gui = _gameBox.GetGui();
            gui.Add(fpsWidget);

            _gameBox.AddTickable(fpsWidget);
        }
    }
}
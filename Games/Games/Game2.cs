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
using RenderCore.Drawable;
using RenderCore.Resource;
using ResourceUtilities.Aseprite;
using SFML.Graphics;
using SFML.System;

namespace Games.Games
{
    public class Game2 : GameBase
    {
        public Game2()
        {
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

                manEntity = SpriteEntityFactory.CreateSpriteEntity(mass, manPosition, BodyType.Dynamic,
                    sprite);

                m_drawables.Add(manEntity);
                m_tickables.Add(manEntity);
                m_bodies.Add(manEntity);
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

                IMap map = new SampleMap2(mapSceneTexture, mapCollisionGrid);

                foreach (IEntity mapEntity in map.GetEntities())
                {
                    m_drawables.Add(mapEntity);
                    m_tickables.Add(mapEntity);
                    m_bodies.Add(mapEntity);
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
            List<Coin> coins = CoinEntitiesFactory.GetCoins(resourceRootDirectory).ToList();

            CoinThing coinThing = new CoinThing(manEntity, coins);
            AddGameProvider(coinThing);
        }
    }
}
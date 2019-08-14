using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Common.Grid;
using GameCore;
using GameCore.Maps;
using GameResources.Attributes;
using GameResources.Converters;
using Games.Maps;
using LibExtensions;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Resource;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using ResourceUtilities.Aseprite;
using SFML.Graphics;

namespace Games.Games
{
    public class Game3 : IDisposable
    {
        private readonly GameCore.GameBox m_gameBox;

        public Game3()
        {
            m_gameBox = new GameCore.GameBox();

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
            LabeledGridWidget gridWidget =
                new LabeledGridWidget(viewProvider, 0.5f * Vector2.One, gridLabelFontSettings);

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
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Common.Grid;
using Common.Tickable;
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
    public class Game3 : GameBase
    {
        public Game3()
        {
            const float size = 25;
            Vector2 sceneSize = new Vector2(size, size);
            Vector2 scenePosition = -Vector2.One;
            FloatRect viewRect = new FloatRect(scenePosition.GetVector2F(), sceneSize.GetVector2F());
            View view = new View(viewRect);

            m_viewProvider = new ViewProviderBase(view);

            //MultiDrawable<VertexArrayShape> box = DrawableFactory.GetBox(sceneSize, Color.White);
            //m_drawables.Add(box);

            WidgetFontSettings widgetFontSettings = new WidgetFontSettings();
            FontSettings gridLabelFontSettings = widgetFontSettings.GetSettings(WidgetFontSettingsType.LABELED_GRID);
            LabeledGridWidget gridWidget =
                new LabeledGridWidget(m_viewProvider, 0.5f * Vector2.One, gridLabelFontSettings);

            m_drawables.Add(gridWidget);

            MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One);
            m_drawables.Add(crossHairs);

            m_tickables.Add(gridWidget);
            
            ResourceManager<SpriteResources> manager =
                new ResourceManager<SpriteResources>(@"C:\dev\GameBox\Resources\sprite");

            Resource<Texture> mapSceneResource = manager.GetTextureResource(SpriteResources.MAP_TREE_SCENE);
            Texture mapSceneTexture = mapSceneResource.Load();

            Resource<Bitmap> mapCollisionResource = manager.GetBitmapResource(SpriteResources.MAP_TREE_COLLISION);
            Bitmap mapCollisionBitmap = mapCollisionResource.Load();

            Grid<ComparableColor> mapCollisionGrid = BitmapToGridConverter.GetColorGridFromBitmap(mapCollisionBitmap);

            IMap map = new SampleMap2(mapSceneTexture, mapCollisionGrid);

            IEnumerable<IDrawable> mapDrawables = map.GetDrawables();
            foreach (IDrawable mapDrawable in mapDrawables)
            {
                m_drawables.Add(mapDrawable);
            }
        }
    }
}
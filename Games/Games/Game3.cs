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
using RenderCore.Render;
using RenderCore.Resource;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using ResourceUtilities.Aseprite;
using SFML.Graphics;

namespace Games.Games
{
    public class Game3 : GameBase
    {
        public Game3() : this(SceneProviderFactory.CreateSceneProvider())
        {
            
        }
        public Game3(ISceneProvider _sceneProvider) : base(_sceneProvider)
        {
            const float size = 25;
            Vector2 sceneSize = new Vector2(size, size);
            Vector2 scenePosition = -Vector2.One;
            FloatRect viewRect = new FloatRect(scenePosition.GetVector2F(), sceneSize.GetVector2F());
            View view = new View(viewRect);

            ViewProviderBase viewProvider = new ViewProviderBase(view);

            //MultiDrawable<VertexArrayShape> box = DrawableFactory.GetBox(sceneSize, Color.White);
            //m_drawables.Add(box);

            WidgetFontSettings widgetFontSettings = new WidgetFontSettings();
            FontSettings gridLabelFontSettings = widgetFontSettings.GetSettings(WidgetFontSettingsType.LABELED_GRID);
            LabeledGridWidget gridWidget =
                new LabeledGridWidget(viewProvider, 0.5f * Vector2.One, gridLabelFontSettings);

            SetViewProvider(viewProvider);

            AddDrawable(gridWidget);

            MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One);
            AddDrawable(crossHairs);

            AddDrawable(gridWidget);

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
                AddDrawable(mapDrawable);
            }
        }
    }
}
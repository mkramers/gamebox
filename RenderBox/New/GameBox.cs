using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Common.Grid;
using Common.Tickable;
using GameBox;
using GameCore.Maps;
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

    public class Game2 : IDisposable
    {
        private readonly GameBox m_gameBox;

        public Game2()
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

            //m_gameRunner.AddFpsWidget();

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

        public void InvokeGui(Action<Gui> _guiAction)
        {
            m_gameBox.InvokeGui(_guiAction);
        }

        public void AddDrawableProvider(IDrawableProvider _drawableProvider)
        {
            m_gameBox.AddDrawableProvider(_drawableProvider);
        }

        public void AddTickable(ITickable _tickable)
        {
            m_gameBox.AddTickable(_tickable);
        }

        public void InvokePhysics(Action<IPhysics> _action)
        {
            _action(m_physics);
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
    }
}
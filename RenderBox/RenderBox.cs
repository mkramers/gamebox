using System;
using System.Collections.Generic;
using System.Numerics;
using GameCore;
using GameCore.Maps;
using LibExtensions;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using SFML.Graphics;
using SFML.System;

namespace RenderBox
{
    public class RenderBox : IDisposable
    {
        private readonly GameRunner m_gameRunner;

        public RenderBox(string _windowTitle, Vector2u _windowSize, float _aspectRatio)
        {
            m_gameRunner = new GameRunner(_windowTitle, _windowSize, Vector2.Zero, _aspectRatio);

            const float size = 25;
            Vector2 sceneSize = new Vector2(size, size);
            Vector2 scenePosition = -Vector2.One;
            FloatRect viewRect = new FloatRect(scenePosition.GetVector2F(), sceneSize.GetVector2F());
            View view = new View(viewRect);

            ViewProviderBase viewProvider = new ViewProviderBase(view);
            m_gameRunner.SetSceneViewProvider(viewProvider);

            //MultiDrawable<RectangleShape> box = DrawableFactory.GetBox(sceneSize, 1);
            //scene.AddDrawable(box);

            WidgetFontSettings widgetFontSettings = new WidgetFontSettings();
            FontSettings gridLabelFontSettings = widgetFontSettings.GetSettings(WidgetFontSettingsType.LABELED_GRID);
            LabeledGridWidget gridWidget =
                new LabeledGridWidget(viewProvider, 0.05f, 0.5f * Vector2.One, gridLabelFontSettings);
            m_gameRunner.AddDrawableToScene(gridWidget);

            MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One, 0.1f);
            m_gameRunner.AddDrawableToScene(crossHairs);

            m_gameRunner.AddWidget(gridWidget);

            m_gameRunner.AddFpsWidget();

            //const string mapName = "square";
            const string mapName = "sample_tree_map";
            string mapFilePath = $@"C:\dev\GameBox\RenderCore\Resources\art\{mapName}.json";

            SampleMap2 map = new SampleMap2(mapFilePath, m_gameRunner.GetPhysics());

            IEnumerable<IDrawable> mapDrawables = map.GetDrawables();
            foreach (IDrawable mapDrawable in mapDrawables)
            {
                m_gameRunner.AddDrawableToScene(mapDrawable);
            }
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
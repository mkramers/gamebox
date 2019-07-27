using System.Collections.Generic;
using System.Numerics;
using GameCore;
using GameCore.Maps;
using LibExtensions;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Render;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using SFML.Graphics;
using SFML.System;

namespace RenderBox
{
    public class RenderBox : Game
    {
        public RenderBox(string _windowTitle, Vector2u _windowSize, float _aspectRatio) : base(_windowTitle,
            _windowSize, Vector2.Zero, _aspectRatio)
        {
            const float size = 25;
            Vector2 sceneSize = new Vector2(size, size);
            Vector2 scenePosition = -Vector2.One;
            FloatRect viewRect = new FloatRect(scenePosition.GetVector2F(), sceneSize.GetVector2F());
            View view = new View(viewRect);

            RenderCoreWindow renderCoreWindow = RenderCoreWindow;
            IRenderCoreTarget scene = renderCoreWindow.GetScene();

            ViewProviderBase viewProvider = new ViewProviderBase(view);
            scene.SetViewProvider(viewProvider);

            //MultiDrawable<RectangleShape> box = DrawableFactory.GetBox(sceneSize, 1);
            //scene.AddDrawable(box);

            WidgetFontSettings widgetFontSettings = new WidgetFontSettings();
            FontSettings gridLabelFontSettings = widgetFontSettings.GetSettings(WidgetFontSettingsType.LABELED_GRID);
            LabeledGridWidget gridWidget =
                new LabeledGridWidget(viewProvider, 0.05f, 0.5f * Vector2.One, gridLabelFontSettings);
            scene.AddDrawable(gridWidget);

            MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One, 0.1f);
            scene.AddDrawable(crossHairs);

            AddWidget(gridWidget);

            AddFpsWidget(renderCoreWindow);

            //const string mapName = "square";
            const string mapName = "sample_tree_map";
            string mapFilePath = $@"C:\dev\GameBox\RenderCore\Resources\art\{mapName}.json";

            SampleMap2 map = new SampleMap2(mapFilePath, Physics);

            IEnumerable<IDrawable> mapDrawables = map.GetDrawables();
            foreach (IDrawable mapDrawable in mapDrawables)
            {
                scene.AddDrawable(mapDrawable);
            }
        }

        private void AddFpsWidget(RenderCoreWindow _renderCoreWindow)
        {
            WidgetFontSettings widgetFontSettingsFactory = new WidgetFontSettings();
            FontSettings fpsFontSettings = widgetFontSettingsFactory.GetSettings(WidgetFontSettingsType.FPS_COUNTER);

            Vector2 textPosition = new Vector2(fpsFontSettings.Scale, 1.0f - 1.5f * fpsFontSettings.Scale);

            Text text = TextFactory.GenerateText(fpsFontSettings);
            text.Position = textPosition.GetVector2F();

            FpsTextWidget fpsTextWidget = new FpsTextWidget(5, text);

            IRenderCoreTarget overlay = _renderCoreWindow.GetOverlay();
            overlay.AddDrawable(fpsTextWidget);

            AddWidget(fpsTextWidget);
        }
    }
}
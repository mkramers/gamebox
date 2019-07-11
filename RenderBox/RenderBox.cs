using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Common.Extensions;
using Common.Geometry;
using Common.VertexObject;
using GameCore;
using GameCore.Maps;
using GameResources;
using GameResources.Attributes;
using LibExtensions;
using MarchingSquares;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Render;
using RenderCore.ShapeUtilities;
using RenderCore.TextureCache;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;

namespace RenderBox
{
    public class RenderBox : Game
    {
        public RenderBox(string _windowTitle, Vector2u _windowSize, float _aspectRatio) : base(_windowTitle,
            _windowSize, Vector2.Zero, _aspectRatio)
        {
            const float size = 25;
            Vector2 sceneSize = new Vector2(size, size);
            Vector2 scenePosition = /*-sceneSize / 2.0f +*/ -Vector2.One;
            FloatRect viewRect = new FloatRect(scenePosition.GetVector2F(), sceneSize.GetVector2F());
            View view = new View(viewRect);

            RenderCoreWindow renderCoreWindow = RenderCoreWindow;
            IRenderCoreTarget scene = renderCoreWindow.GetScene();

            ViewProviderBase viewProvider = new ViewProviderBase(view);
            scene.SetViewProvider(viewProvider);

            //MultiDrawable<RectangleShape> box = DrawableFactory.GetBox(sceneSize, 1);
            //scene.AddDrawable(box);

            GridWidget gridWidget = new GridWidget(viewProvider, 0.05f, 0.5f * Vector2.One);
            scene.AddDrawable(gridWidget);

            MultiDrawable<VertexArrayShape> crossHairs = DrawableFactory.GetCrossHair(5 * Vector2.One, 0.1f);
            scene.AddDrawable(crossHairs);

            AddWidget(gridWidget);

            AddFpsWidget(renderCoreWindow);

            const string mapName = "sample_tree_map";
            string mapFilePath = $@"C:\dev\GameBox\RenderCore\Resources\art\{mapName}.json";

            SampleMap2 map = new SampleMap2(mapFilePath, Physics);

            MultiDrawable<VertexArrayShape> lineDrawables = map.LineDrawable;
            scene.AddDrawable(lineDrawables);
        }

        private void AddFpsWidget(RenderCoreWindow _renderCoreWindow)
        {
            const float fontScale = 0.025f;
            const uint fontSize = 32;
            Vector2 textPosition = new Vector2(fontScale, 1.0f - 1.5f * fontScale);

            FontFactory fontFactory = new FontFactory();
            Font font = fontFactory.GetFont(FontId.ROBOTO);

            Text textRenderObject = new Text("", font, fontSize)
            {
                Scale = new Vector2f(fontScale / fontSize, fontScale / fontSize),
                FillColor = Color.Blue
            };
            FpsTextWidget fpsTextWidget = new FpsTextWidget(5, textRenderObject);
            fpsTextWidget.SetPosition(textPosition);

            IRenderCoreTarget overlay = _renderCoreWindow.GetOverlay();
            overlay.AddDrawable(fpsTextWidget);

            AddWidget(fpsTextWidget);
        }
    }
}
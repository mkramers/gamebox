using System.Numerics;
using GameCore;
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
            const float size = 10;
            Vector2 sceneSize = new Vector2(size, size);
            Vector2 scenePosition = new Vector2();
            FloatRect viewRect = new FloatRect(scenePosition.GetVector2F(), sceneSize.GetVector2F());
            View view = new View(viewRect);

            RenderCoreWindow renderCoreWindow = RenderCoreWindow;
            IRenderCoreTarget scene = renderCoreWindow.GetScene();

            ViewProviderBase viewProvider = new ViewProviderBase(view);
            scene.SetViewProvider(viewProvider);

            MultiDrawable<RectangleShape> box = DrawableFactory.GetBox(sceneSize, 1);

            scene.AddDrawable(box);

            GridWidget gridWidget = new GridWidget(viewProvider, 0.05f, new Vector2(1, 1));
            scene.AddDrawable(gridWidget);

            AddWidget(gridWidget);

            AddFpsWidget(renderCoreWindow);
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
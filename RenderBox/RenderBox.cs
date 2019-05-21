using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using RenderCore;
using SFML.Graphics;
using SFML.System;

namespace RenderBox
{
    public class RenderBox
    {
        public RenderBox(string _windowTitle, Vector2u _windowSize)
        {
            RenderCoreWindow renderCoreWindow =
                RenderCoreWindowFactory.CreateRenderCoreWindow(_windowTitle, _windowSize);

            const float size = 10;
            Vector2 sceneSize = new Vector2(size, size);
            Vector2 scenePosition = new Vector2();
            FloatRect viewRect = new FloatRect(scenePosition.GetVector2F(), sceneSize.GetVector2F());
            View view = new View(viewRect);

            IRenderCoreTarget scene = renderCoreWindow.GetScene();

            ViewProviderBase viewProvider = new ViewProviderBase(view);
            scene.SetViewProvider(viewProvider);

            DrawBox(sceneSize, scene);

            TickableContainer<IWidget> widgets = new TickableContainer<IWidget>();

            GridWidget gridWidget = new GridWidget(viewProvider);
            scene.AddDrawable(gridWidget);

            widgets.Add(gridWidget);

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

            IRenderCoreTarget overlay = renderCoreWindow.GetOverlay();
            overlay.AddDrawable(fpsTextWidget);
            widgets.Add(fpsTextWidget);

            Stopwatch stopwatch = Stopwatch.StartNew();

            while (true)
            {
                if (!renderCoreWindow.IsOpen)
                {
                    break;
                }

                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();

                widgets.Tick(elapsed);

                renderCoreWindow.Tick(elapsed);

                Thread.Sleep(30);
            }
        }

        private static void DrawBox(Vector2 _sceneSize, IRenderObjectContainer _scene)
        {
            {
                LineSegment lineSegment = new LineSegment(new Vector2(0, 0), new Vector2(_sceneSize.X, 0));
                DrawLine(_scene, lineSegment);
            }
            {
                LineSegment lineSegment = new LineSegment(new Vector2(0, 0), new Vector2(0, _sceneSize.Y));
                DrawLine(_scene, lineSegment);
            }
            {
                LineSegment lineSegment =
                    new LineSegment(new Vector2(_sceneSize.X, 0), new Vector2(_sceneSize.X, _sceneSize.Y));
                DrawLine(_scene, lineSegment);
            }
            {
                LineSegment lineSegment =
                    new LineSegment(new Vector2(0, _sceneSize.Y), new Vector2(_sceneSize.X, _sceneSize.Y));
                DrawLine(_scene, lineSegment);
            }
        }

        private static void DrawLine(IRenderObjectContainer _scene, LineSegment _lineSegment)
        {
            ShapeDrawable lineSegmentDrawable = DrawableFactory.GetLineSegment(_lineSegment, 1);
            lineSegmentDrawable.SetFillColor(Color.Red);
            _scene.AddDrawable(lineSegmentDrawable);
        }
    }
}
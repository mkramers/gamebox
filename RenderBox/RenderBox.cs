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

            const float SIZE = 10;
            Vector2 sceneSize = new Vector2(SIZE, SIZE);
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

            const float FONT_SCALE = 0.025f;
            const uint fontSize = 32;
            Vector2 textPosition = new Vector2(FONT_SCALE, 1.0f - 1.5f * FONT_SCALE);

            FontFactory fontFactory = new FontFactory();
            Font font = fontFactory.GetFont(FontId.ROBOTO);

            Text textRenderObject = new Text("", font, fontSize)
            {
                Scale = new Vector2f(FONT_SCALE / fontSize, FONT_SCALE / fontSize),
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

        private static void DrawBox(Vector2 sceneSize, IRenderCoreTarget scene)
        {
            {
                LineSegment lineSegment = new LineSegment(new Vector2(0, 0), new Vector2(sceneSize.X, 0));
                DrawLine(scene, lineSegment);
            }
            {
                LineSegment lineSegment = new LineSegment(new Vector2(0, 0), new Vector2(0, sceneSize.Y));
                DrawLine(scene, lineSegment);
            }
            {
                LineSegment lineSegment =
                    new LineSegment(new Vector2(sceneSize.X, 0), new Vector2(sceneSize.X, sceneSize.Y));
                DrawLine(scene, lineSegment);
            }
            {
                LineSegment lineSegment =
                    new LineSegment(new Vector2(0, sceneSize.Y), new Vector2(sceneSize.X, sceneSize.Y));
                DrawLine(scene, lineSegment);
            }
        }

        private static void DrawLine(IRenderCoreTarget scene, LineSegment lineSegment)
        {
            ShapeDrawable lineSegmentDrawable = DrawableFactory.GetLineSegment(lineSegment, 1);
            lineSegmentDrawable.SetFillColor(Color.Red);
            scene.AddDrawable(lineSegmentDrawable);
        }
    }
}
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

            IRenderCoreTarget scene = renderCoreWindow.GetScene();
            
            LineSegment lineSegment = new LineSegment(new Vector2(5, 5), new Vector2(45, 25));
            ShapeDrawable lineSegmentDrawable = DrawableFactory.GetLineSegment(lineSegment, 1);
            lineSegmentDrawable.SetFillColor(Color.Red);
            scene.AddDrawable(lineSegmentDrawable);
            
            IViewProvider viewProvider = renderCoreWindow.GetViewProvider();

            GridWidget gridWidget = new GridWidget(viewProvider);
            scene.AddWidget(gridWidget);

            FontFactory fontFactory = new FontFactory();
            Font font = fontFactory.GetFont(FontId.ROBOTO);
            Vector2 textPosition = new Vector2(0.1f, 0.1f);

            ViewSpaceConverter viewSpaceConverter = new ViewSpaceConverter(viewProvider);
            const uint fontSize = 32;
            Text textRenderObject = new Text("", font, fontSize);
            FpsTextWidget fpsTextWidget = new FpsTextWidget(5, textRenderObject);
            fpsTextWidget.SetPosition(textPosition);

            IRenderCoreTarget overlay = renderCoreWindow.GetOverlay();
            overlay.AddWidget(fpsTextWidget);

            Stopwatch stopwatch = Stopwatch.StartNew();

            while (true)
            {
                if (!renderCoreWindow.IsOpen)
                {
                    break;
                }

                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();

                fpsTextWidget.Tick(elapsed);

                renderCoreWindow.Tick(elapsed);

                Thread.Sleep(30);
            }
        }
    }
}
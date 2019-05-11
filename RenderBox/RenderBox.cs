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
            FloatRect viewRect = new FloatRect(new Vector2f(0, 0), new Vector2f(50, 50));
            RenderCoreWindow renderCoreWindow =
                RenderCoreWindowFactory.CreateRenderCoreWindow(_windowTitle, _windowSize, viewRect);

            LineSegment lineSegment = new LineSegment(new Vector2(5, 5), new Vector2(45, 25));
            ShapeDrawable lineSegmentDrawable = DrawableFactory.GetLineSegment(lineSegment, 1);
            lineSegmentDrawable.SetColor(Color.Red);
            renderCoreWindow.Add(lineSegmentDrawable);

            GridWidget gridWidget = new GridWidget();
            //renderCoreWindow.AddWidget(gridWidget);

            FontFactory fontFactory = new FontFactory();
            Font font = fontFactory.GetFont(FontId.ROBOTO);
            //Vector2 textPosition = new Vector2(0.1f, 0.9f);
            Vector2 textPosition = new Vector2(5, 45);
            FpsTextWidget fpsTextWidget = new FpsTextWidget(font, 24, 5);
            fpsTextWidget.SetRenderPosition(textPosition);
            renderCoreWindow.AddWidget(fpsTextWidget);

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
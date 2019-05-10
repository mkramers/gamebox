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

            LineSegment lineSegment = new LineSegment(new Vector2(-50, 5), new Vector2(50, 5));
            ShapeDrawable lineSegmentDrawable = DrawableFactory.GetLineSegment(lineSegment, 1);
            lineSegmentDrawable.SetColor(Color.Red);
            renderCoreWindow.Add(lineSegmentDrawable);

            GridWidget gridWidget = new GridWidget();

            renderCoreWindow.AddWidget(gridWidget);

            Stopwatch stopwatch = Stopwatch.StartNew();

            while (true)
            {
                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();

                renderCoreWindow.Tick(elapsed);
                Thread.Sleep(30);
            }
        }
    }
}
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
            
            FloatRect viewRect = new FloatRect(-_windowSize.X/2.0f, -_windowSize.Y / 2.0f, _windowSize.X, _windowSize.Y);
            View view = new View(viewRect);
            
            IRenderCoreTarget scene = renderCoreWindow.GetScene();

            ViewProviderBase viewProvider = new ViewProviderBase(view);
            scene.SetViewProvider(viewProvider);
            
            LineSegment lineSegment = new LineSegment(new Vector2(3, 0), new Vector2(10, 0));
            ShapeDrawable lineSegmentDrawable = DrawableFactory.GetLineSegment(lineSegment, 1);
            lineSegmentDrawable.SetFillColor(Color.Red);
            scene.AddDrawable(lineSegmentDrawable);

            TickableContainer<IWidget> widgets = new TickableContainer<IWidget>();
            
            GridWidget gridWidget = new GridWidget(viewProvider);
            scene.AddDrawable(gridWidget);

            widgets.Add(gridWidget);

            FontFactory fontFactory = new FontFactory();
            Font font = fontFactory.GetFont(FontId.ROBOTO);
            Vector2 textPosition = new Vector2(0, 0);

            ViewSpaceConverter viewSpaceConverter = new ViewSpaceConverter(viewProvider);
            const uint fontSize = 32;
            Text textRenderObject = new Text("", font, fontSize);
            textRenderObject.Scale = new Vector2f(0.5f / 32.0f, 0.5f / 32.0f);
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
    }
}
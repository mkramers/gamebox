using System.Numerics;
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

            LineSegment lineSegment = new LineSegment(new Vector2(-5, 5), new Vector2(5, 5));
            ShapeDrawable lineSegmentDrawable = DrawableFactory.GetLineSegment(lineSegment, 0.25f);
            //renderCoreWindow.Add(lineSegmentDrawable);

            while (true)
            {
                renderCoreWindow.Tick();
            }
        }
    }
}
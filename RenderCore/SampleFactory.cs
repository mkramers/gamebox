using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class SampleFactory
    {
        public static MultiDrawable GetSample()
        {
            Vector2 origin = Vector2.Zero;

            IEnumerable<Vector2> positions = LandscapeFactory.GetPyramid(20);
            MultiDrawable multiDrawable = DrawableFactory.CreateMultiDrawable(positions, origin, ResourceId.WOOD);
            return multiDrawable;
        }
    }
}
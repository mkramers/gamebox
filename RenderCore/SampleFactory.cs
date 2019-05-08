using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class SampleFactory
    {
        public static MultiDrawable<SpriteDrawable> GetSample()
        {
            Vector2 origin = Vector2.Zero;

            List<Tuple<SpriteDrawable, Matrix3x2>> drawables = new List<Tuple<SpriteDrawable, Matrix3x2>>();

            IEnumerable<Vector2> positions = LandscapeFactory.GetPyramid(20);
            foreach (Vector2 position in positions)
            {
                Sprite sprite = SpriteFactory.GetSprite(ResourceId.WOOD);
                SpriteDrawable spriteDrawable = new SpriteDrawable(sprite);

                Matrix3x2 transform = Matrix3x2.CreateTranslation(position + origin);

                drawables.Add(new Tuple<SpriteDrawable, Matrix3x2>(spriteDrawable, transform));
            }

            return new MultiDrawable<SpriteDrawable>(drawables);
        }
    }
}
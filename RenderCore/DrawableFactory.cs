using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class DrawableFactory
    {
        public static MultiDrawable CreateMultiDrawable(IEnumerable<Vector2> _positions, Vector2 _origin,
            ResourceId _resourceId)
        {
            List<Tuple<IDrawable, Matrix3x2>> drawables = new List<Tuple<IDrawable, Matrix3x2>>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (Vector2 position in _positions)
            {
                Sprite sprite = SpriteFactory.GetSprite(_resourceId);
                Drawable<Sprite> spriteDrawable = new Drawable<Sprite>(sprite);
                

                Matrix3x2 transform = Matrix3x2.CreateTranslation(position + _origin);

                drawables.Add(new Tuple<IDrawable, Matrix3x2>(spriteDrawable, transform));
            }

            MultiDrawable multiDrawable = new MultiDrawable(drawables);
            return multiDrawable;
        }

        public static ShapeDrawable GetLineSegment(LineSegment _line, float _thickness)
        {
            RectangleShape shape = ShapeFactory.GetLineShape(_line, _thickness);
            ShapeDrawable drawable = new ShapeDrawable(shape);
            return drawable;
        }
    }
}
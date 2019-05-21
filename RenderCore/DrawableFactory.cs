using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class DrawableFactory
    {
        public static MultiDrawable<Sprite> CreateMultiDrawable(IEnumerable<Vector2> _positions, Vector2 _origin,
            ResourceId _resourceId)
        {
            List<Drawable<Sprite>> drawables = new List<Drawable<Sprite>>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (Vector2 position in _positions)
            {
                Sprite sprite = SpriteFactory.GetSprite(_resourceId);
                sprite.Position = (position + _origin).GetVector2F();

                drawables.Add(new Drawable<Sprite>(sprite));
            }

            MultiDrawable<Sprite> multiDrawable = new MultiDrawable<Sprite>(drawables);
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
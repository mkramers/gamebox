using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class FloatRectExtensions
    {
        public static IVertexObject GetPolygon(this FloatRect _rect)
        {
            Vector2 position = _rect.GetPosition();
            Vector2 size = _rect.GetSize();
            Polygon polygon = ShapeFactory.CreateRectangle(size);

            IVertexObject vertexObject = Polygon.Translate(polygon, position);
            return vertexObject;
        }

        public static Vector2 GetPosition(this FloatRect _rect)
        {
            Vector2 position = new Vector2(_rect.Left, _rect.Top);
            return position;
        }

        public static Vector2 GetSize(this FloatRect _rect)
        {
            Vector2 size = new Vector2(_rect.Width, _rect.Height);
            return size;
        }
    }
}
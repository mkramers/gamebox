using System.Numerics;
using Aether.Physics2D.Dynamics;
using SFML.Graphics;

namespace RenderCore
{
    public static class SpriteEntityFactory
    {
        public static IEntity CreateSpriteEntity(float _mass, Vector2 _position, IPhysics _physics, BodyType _bodyType,
            Sprite _sprite, Vector2 _size)
        {
            Drawable<Sprite> spriteDrawable = new Drawable<Sprite>(_sprite, -_size / 2);

            Polygon bodyPolygon = ShapeFactory.CreateRectangle(Vector2.Zero, _size / 2);

            IBody body = _physics.CreateVertexBody(bodyPolygon, _position, _mass, _bodyType);

            Entity entity = new Entity(spriteDrawable, body);
            return entity;
        }
    }

    public static class EntityFactory
    {
        public static Entity CreatePolygonEntity(IPhysics _physics, Vector2 _position, Polygon _polygon)
        {
            IBody body = _physics.CreateVertexBody(_polygon, _position, 1, BodyType.Static);

            ConvexShape shape = ShapeFactory.GetConvexShape(_polygon);

            Drawable<ConvexShape> drawable = new Drawable<ConvexShape>(shape);

            Entity entity = new Entity(drawable, body);
            return entity;
        }
    }
}
using System.Numerics;
using Aether.Physics2D.Dynamics;
using SFML.Graphics;

namespace RenderCore
{
    public static class EntityFactory
    {
        public static IEntity CreateEntity(float _mass, Vector2 _position, IPhysics _physics, ResourceId _resourceId,
            BodyType _bodyType)
        {
            Vector2 size = Vector2.One;

            Sprite sprite = SpriteFactory.GetSprite(_resourceId);

            Drawable<Sprite> spriteDrawable = new Drawable<Sprite>(sprite, -size / 2);

            Polygon bodyPolygon = ShapeFactory.CreateRectangle(Vector2.Zero, size / 2);

            IBody body = _physics.CreateVertexBody(bodyPolygon, _position, _mass, _bodyType);

            Entity entity = new Entity(spriteDrawable, body);
            return entity;
        }

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
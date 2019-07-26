using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Geometry;
using Common.VertexObject;
using LibExtensions;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.ShapeUtilities;
using SFML.Graphics;

namespace GameCore.Entity
{
    public static class SpriteEntityFactory
    {
        public static IEntity CreateSpriteEntity(float _mass, Vector2 _position, IPhysics _physics, BodyType _bodyType,
            Sprite _sprite)
        {
            FloatRect spriteLocalBounds = _sprite.GetGlobalBounds();

            Vector2 spriteSize = spriteLocalBounds.GetSize();

            IVertexObject bodyVertexObject = ShapeFactory.CreateRectangle(spriteSize / 2);

            IEntity entity =
                CreateSpriteEntity(_mass, _position, _physics, _bodyType, _sprite, bodyVertexObject);
            return entity;
        }

        public static IEntity CreateSpriteEntity(float _mass, Vector2 _position, IPhysics _physics, BodyType _bodyType,
            Sprite _sprite, IVertexObject _bodyVertexObject)
        {
            Drawable<Sprite> spriteDrawable = new Drawable<Sprite>(_sprite);

            IBody body = _physics.CreateVertexBody(_bodyVertexObject, _position, _mass, _bodyType);

            Entity entity = new Entity(spriteDrawable, body);
            return entity;
        }
    }
}
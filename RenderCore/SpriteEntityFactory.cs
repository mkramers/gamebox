using System.Numerics;
using Aether.Physics2D.Dynamics;
using SFML.Graphics;

namespace RenderCore
{
    public static class SpriteEntityFactory
    {
        public static IEntity CreateSpriteEntity(float _mass, Vector2 _position, Vector2 _scale, IPhysics _physics, BodyType _bodyType,
            Sprite _sprite)
        {
            FloatRect spriteLocalBounds = _sprite.GetGlobalBounds();

            Vector2 spriteSize = spriteLocalBounds.GetSize();

            Drawable<Sprite> spriteDrawable = new Drawable<Sprite>(_sprite, -spriteSize / 2);

            IVertexObject bodyVertexObject = ShapeFactory.CreateRectangle(spriteSize / 2);

            IBody body = _physics.CreateVertexBody(bodyVertexObject, _position + spriteSize / 2, _mass, _bodyType);

            Entity entity = new Entity(spriteDrawable, body);
            return entity;
        }
    }
}
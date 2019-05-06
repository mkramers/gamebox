using System.Numerics;
using Aether.Physics2D.Dynamics;
using SFML.Graphics;

namespace RenderCore
{
    public static class EntityFactory
    {
        public static IEntity CreateEntity(float _mass, Vector2 _position, Physics _physics, ResourceId _resourceId,
            BodyType _bodyType)
        {
            Sprite sprite = SpriteFactory.GetSprite(_resourceId);

            IBody body = _physics.CreateBody(_position, _mass, _bodyType);

            Entity entity = new Entity(sprite, body);
            return entity;
        }
    }
}
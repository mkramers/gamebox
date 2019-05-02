using System.Numerics;
using Aether.Physics2D.Dynamics;
using SFML.Graphics;

namespace RenderCore
{
    public static class EntityFactory
    {
        public static IEntity CreateEntity(float _mass, Vector2 _position, Physics2 _physics2, ResourceId _resourceId,
            BodyType _bodyType)
        {
            Sprite sprite = SpriteFactory.GetSprite(_resourceId);

            IBody body = _physics2.CreateBody(_position, _mass, _bodyType);

            Entity entity = new Entity(sprite, body);
            return entity;
        }
    }
}
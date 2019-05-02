using System.Numerics;
using Aether.Physics2D.Dynamics;
using SFML.Graphics;

namespace RenderCore
{
    public class EntityFactory
    {
        public static IEntity CreateEntity(float _mass, Vector2 _position, Physics2 _physics2, ResourceId _resourceId,
            BodyType _bodyType)
        {
            SpriteFactory spriteFactory = new SpriteFactory();
            Sprite sprite = spriteFactory.GetSprite(_resourceId);

            IBody body = _physics2.CreateBody(_position, _mass, _bodyType);

            Entity entity = new Entity(sprite, body);
            return entity;
        }
    }
}
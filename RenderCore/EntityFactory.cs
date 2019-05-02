using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class EntityFactory
    {
        public IEntity CreateEntity(float _mass, Physics2 _physics2, ResourceId _resourceId)
        {
            SpriteFactory spriteFactory = new SpriteFactory();
            Sprite sprite = spriteFactory.GetSprite(_resourceId);

            IBody body = _physics2.CreateDynamicBody(_mass);

            Entity entity = new Entity(sprite, body);
            return entity;
        }
    }
}
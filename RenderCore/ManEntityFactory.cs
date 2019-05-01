using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class ManEntityFactory
    {
        public IDynamicEntity GetMan(float _mass, Physics2 _physics2)
        {
            SpriteFactory spriteFactory = new SpriteFactory();
            Sprite sprite = spriteFactory.GetSprite(ResourceId.MAN);

            IDynamicBody dynamicBody = _physics2.CreateDynamicBody(_mass);

            IDynamicEntity dynamicEntity = new DynamicBodyEntity(sprite, dynamicBody);
            return dynamicEntity;
        }
    }

    public class LogEntityFactory
    {
        public IStaticEntity GetLog(Physics2 _physics2, Vector3 _position)
        {
            SpriteFactory spriteFactory = new SpriteFactory();
            Sprite sprite = spriteFactory.GetSprite(ResourceId.WOOD);

            IStaticBody staticBody = _physics2.CreateStaticBody(_position);

            IStaticEntity staticEntity = new StaticBodyEntity(sprite, staticBody);
            staticEntity.Tick(0);
            return staticEntity;

        }
    }
}
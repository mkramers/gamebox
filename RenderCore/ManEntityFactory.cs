using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class ManEntityFactory
    {
        public IEntity GetMan(float _mass, Physics2 _physics2)
        {
            CoreSpriteFactory spriteFactory = new CoreSpriteFactory();
            Sprite sprite = spriteFactory.GetBodySprite(ResourceId.MAN);

            IPhysicalBody physicalBody = _physics2.CreatePhysicalObject(_mass);

            IEntity entity = new PhysicalBodyEntity(sprite, physicalBody);
            return entity;
        }
    }
}
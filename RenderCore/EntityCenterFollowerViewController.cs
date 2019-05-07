using System;
using System.Numerics;

namespace RenderCore
{
    public class EntityCenterFollowerViewController : ViewController
    {
        private readonly IEntity m_entity;

        public EntityCenterFollowerViewController(Vector2 _size, IEntity _entity) : base(_size)
        {
            m_entity = _entity;
        }

        public override void Tick(TimeSpan _elapsed)
        {
            Vector2 entityPosition = m_entity.GetPosition();

            SetCenter(entityPosition);
        }
    }
}
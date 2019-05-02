using System;
using System.Collections.Generic;
using System.Numerics;

namespace RenderCore
{
    public class EntityPhysics : Physics2
    {
        private readonly List<IEntity> m_entities;

        public EntityPhysics(Vector2 _gravity) : base(_gravity)
        {
            m_entities = new List<IEntity>();
        }

        public override void Tick(TimeSpan _elapsed)
        {
            base.Tick(_elapsed);

            foreach (IEntity entity in m_entities)
            {
                entity.Tick(_elapsed);
            }
        }

        public void Add(IEntity _entity)
        {
            m_entities.Add(_entity);
        }
    }
}
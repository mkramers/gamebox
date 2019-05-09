using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class EntityCenterFollowerViewController : ViewController
    {
        private readonly IEntity m_entity;

        public EntityCenterFollowerViewController(View _view, float _windowRatio, IEntity _entity) : base(_view, _windowRatio)
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
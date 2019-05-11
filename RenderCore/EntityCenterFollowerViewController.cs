using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class EntityCenterFollowerViewController : ViewControllerBase
    {
        private const float MOVEMENT_BUFFER_SIZE = 0.75f;
        private readonly IEntity m_entity;

        public EntityCenterFollowerViewController(View _view, float _windowRatio, IEntity _entity) : base(_view,
            _windowRatio)
        {
            m_entity = _entity;
        }

        public override void Tick(TimeSpan _elapsed)
        {
            View currentView = GetView();

            Vector2f bufferSize = MOVEMENT_BUFFER_SIZE * currentView.Size;

            Vector2f viewPosition = currentView.Center - bufferSize / 2.0f;

            Vector2 entityPosition = m_entity.GetPosition();

            float newCenterX;
            float newCenterY;

            if (entityPosition.X < viewPosition.X)
            {
                newCenterX = currentView.Center.X - (viewPosition.X - entityPosition.X);
            }
            else if (entityPosition.X > viewPosition.X + bufferSize.X)
            {
                newCenterX = currentView.Center.X + (entityPosition.X - (viewPosition.X + bufferSize.X));
            }
            else
            {
                newCenterX = currentView.Center.X;
            }

            if (entityPosition.Y < viewPosition.Y)
            {
                newCenterY = currentView.Center.Y - (viewPosition.Y - entityPosition.Y);
            }
            else if (entityPosition.Y > viewPosition.Y + bufferSize.Y)
            {
                newCenterY = currentView.Center.Y + (entityPosition.Y - (viewPosition.Y + bufferSize.Y));
            }
            else
            {
                newCenterY = currentView.Center.Y;
            }

            Vector2 newCenter = new Vector2(newCenterX, newCenterY);
            SetCenter(newCenter);
        }
    }
}
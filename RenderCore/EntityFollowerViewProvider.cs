using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class EntityFollowerViewProvider : ViewProviderBase, IWidget
    {
        private const float MOVEMENT_BUFFER_SIZE = 0.75f;
        private readonly IEntity m_entity;

        public EntityFollowerViewProvider(IEntity _entity, View _view) : base(_view)
        {
            m_entity = _entity;
        }

        public void Tick(TimeSpan _elapsed)
        {
            Vector2f bufferSize = MOVEMENT_BUFFER_SIZE * Size;

            Vector2f viewPosition = Center - bufferSize / 2.0f;

            Vector2 entityPosition = m_entity.GetPosition();

            float newCenterX;
            float newCenterY;

            if (entityPosition.X < viewPosition.X)
            {
                newCenterX = Center.X - (viewPosition.X - entityPosition.X);
            }
            else if (entityPosition.X > viewPosition.X + bufferSize.X)
            {
                newCenterX = Center.X + (entityPosition.X - (viewPosition.X + bufferSize.X));
            }
            else
            {
                newCenterX = Center.X;
            }

            if (entityPosition.Y < viewPosition.Y)
            {
                newCenterY = Center.Y - (viewPosition.Y - entityPosition.Y);
            }
            else if (entityPosition.Y > viewPosition.Y + bufferSize.Y)
            {
                newCenterY = Center.Y + (entityPosition.Y - (viewPosition.Y + bufferSize.Y));
            }
            else
            {
                newCenterY = Center.Y;
            }

            Center = new Vector2f(newCenterX, newCenterY);
        }
    }
}
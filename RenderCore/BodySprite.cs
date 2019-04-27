using System.Diagnostics;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class BodySprite : Sprite, IPhysicalObject
    {
        public BodySprite(Texture _texture, IPhysicalObject _physics) : base(_texture)
        {
            m_physics = _physics;
        }

        public BodySprite(Texture _texture, IntRect _rectangle, IPhysicalObject _physics) : base(_texture, _rectangle)
        {
            m_physics = _physics;
        }

        private readonly IPhysicalObject m_physics;
        
        public void ApplyForce(IForce _force)
        {
            m_physics.ApplyForce(_force);
        }
        
        public IForce CombineAndDequeueForces()
        {
            return m_physics.CombineAndDequeueForces();
        }

        public void Move(Vector2 _offset)
        {
            Position += _offset.GetVector2f();
        }
    }
}
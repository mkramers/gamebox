using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class BodySprite : Sprite, IPhysicalObject
    {
        private readonly float m_mass;
        private readonly ConcurrentQueue<IForce> m_forceQueue;

        public BodySprite(Texture _texture, float _mass) : base(_texture)
        {
            m_forceQueue = new ConcurrentQueue<IForce>();
            m_mass = _mass;
        }

        public BodySprite(Texture _texture, IntRect _rectangle, float _mass) : base(_texture, _rectangle)
        {
            m_forceQueue = new ConcurrentQueue<IForce>();
            m_mass = _mass;
        }

        
        public void ApplyForce(IForce _force)
        {
            m_forceQueue.Enqueue(_force);
        }
        
        public IForce CombineAndDequeueForces()
        {
            if (!m_forceQueue.TryDequeue(out IForce resultantForce))
            {
                return null;
            }

            for (int i = 0; i < m_forceQueue.Count; i++)
            {
                if (!m_forceQueue.TryDequeue(out IForce force))
                {
                    resultantForce.Add(force);
                }
            }

            return resultantForce;
        }

        public void Move(Vector2 _offset)
        {
            Position += _offset.GetVector2f();
        }
    }
}
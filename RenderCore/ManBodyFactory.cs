using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class ManBodyFactory
    {
        public BodySprite GetMan(float _mass)
        {
            CoreSpriteFactory spriteFactory = new CoreSpriteFactory();
            IntRect textureCrop = new IntRect(50, 24, 24, 24);;
            BodySprite sprite = spriteFactory.GetBodySprite(ResourceId.MAN, textureCrop, _mass);
            sprite.Scale = new Vector2f(1.0f / textureCrop.Width, 1.0f / textureCrop.Height);
            return sprite;
        }
    }

    public class SpritePhysics : IPhysicalObject
    {
        private readonly float m_mass;
        private readonly ConcurrentQueue<IForce> m_forceQueue;

        public SpritePhysics(float _mass)
        {
            m_mass = _mass;
            m_forceQueue = new ConcurrentQueue<IForce>();
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
        }
    }
    
    public class NormalForce : IForce
    {
        public Vector2 ForceVector { get; private set; }

        public NormalForce(Vector2 _forceVector)
        {
            ForceVector = _forceVector;
        }

        public void Add(IForce _force)
        {
            NormalForce normalForce = _force as NormalForce;
            Debug.Assert(normalForce != null);

            ForceVector += normalForce.ForceVector;
        }

        public void Subtract(IForce _force)
        {
            NormalForce normalForce = _force as NormalForce;
            Debug.Assert(normalForce != null);

            ForceVector -= normalForce.ForceVector;
        }
    }

    public interface IForce
    {
        void Add(IForce _force);
        void Subtract(IForce _force);
    }

    public interface IPhysicalObject
    {
        void ApplyForce(IForce _force);
        IForce CombineAndDequeueForces();
        void Move(Vector2 _offset);
    }
}
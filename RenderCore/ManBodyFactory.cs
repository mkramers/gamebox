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
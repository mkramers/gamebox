using System;
using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using LibExtensions;

namespace PhysicsCore
{
    public class Physics : IPhysics
    {
        private readonly List<IBody> m_bodies;

        public Physics(Vector2 _gravity)
        {
            m_bodies = new List<IBody>();

            World = new World(_gravity.GetVector2());
        }

        public Physics(float _gravityX, float _gravityY) : this(new Vector2(_gravityX, _gravityY))
        {
        }

        private World World { get; }

        public void Dispose()
        {
            World.Clear();
        }

        public void Tick(TimeSpan _elapsed)
        {
            World.Step(_elapsed);
        }

        public void Add(IBody _body)
        {
            m_bodies.Add(_body);
            World.Add(_body.GetBody());
        }

        public void SetGravity(Vector2 _gravity)
        {
            World.Gravity = _gravity.GetVector2();
        }

        public void Remove(IBody _body)
        {
            m_bodies.Remove(_body);
            World.Remove(_body.GetBody());
        }

        public IEnumerable<IBody> GetBodies()
        {
            return m_bodies;
        }
    }
}
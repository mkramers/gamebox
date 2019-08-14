using System;
using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Geometry;
using Common.Tickable;
using Common.VertexObject;

namespace PhysicsCore
{
    public interface IPhysics : ITickable, IDisposable
    {
        void Add(IBody _body);
        void SetGravity(Vector2 _gravity);
        void Remove(IBody _body);
        IEnumerable<IBody> GetBodies();
    }
}
using System;
using Aether.Physics2D.Dynamics;

namespace PhysicsCore
{
    public interface ICollidable
    {
        event EventHandler<CollisionEventArgs> Collided;
        event EventHandler<SeparationEventArgs> Separated;

        bool ContainsFixture(Fixture _fixture);
    }
}
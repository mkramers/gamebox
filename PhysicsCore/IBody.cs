using System.Numerics;
using Common.Geometry;

namespace PhysicsCore
{
    public interface IBody : IPosition, ICollidable
    {
        void ApplyForce(Vector2 _force);

        void ApplyLinearImpulse(Vector2 _force);

        void RemoveFromWorld();
    }
}
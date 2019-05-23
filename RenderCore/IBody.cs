using System.Numerics;

namespace RenderCore
{
    public interface IBody : IPosition
    {
        void ApplyForce(Vector2 _force);

        void ApplyLinearImpulse(Vector2 _force);

        void RemoveFromWorld();
    }
}
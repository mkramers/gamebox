using System.Numerics;

namespace RenderCore
{
    public interface IPosition
    {
        Vector2 GetPosition();
        void SetPosition(Vector2 _position);
    }

    public interface IBody : IPosition
    {
        void ApplyForce(Vector2 _force);

        void ApplyLinearImpulse(Vector2 _force);

        void RemoveFromWorld();
    }
}
using System.Numerics;

namespace RenderCore
{
    public interface IBody
    {
        Vector2 GetPosition();
        void ApplyForce(Vector2 _force);

        void ApplyLinearImpulse(Vector2 _force);

        void RemoveFromWorld();
        void SetPosition(Vector2 _position);
    }
}
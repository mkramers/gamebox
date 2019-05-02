using System.Numerics;

namespace RenderCore
{
    public interface IBody
    {
        Vector2 GetPosition();
        void ApplyForce(NormalForce _force);

        void RemoveFromWorld();
    }
}
using System.Numerics;

namespace RenderCore
{
    public interface IPhysicalObject
    {
        void ApplyForce(IForce _force);
        IForce CombineAndDequeueForces();
        void Move(Vector2 _offset);
    }
}
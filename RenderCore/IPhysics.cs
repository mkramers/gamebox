using System.Numerics;
using Aether.Physics2D.Dynamics;

namespace RenderCore
{
    public interface IPhysics : ITickable
    {
        IBody CreateBody(Vector2 _position, float _mass, BodyType _bodyType);
    }
}
using System.Numerics;
using Aether.Physics2D.Dynamics;

namespace RenderCore
{
    public interface IPhysics : ITickable
    {
        void SetGravity(Vector2 _gravity);
        IBody CreateVertexBody(IVertexObject _vertexObject, Vector2 _position, float _mass, BodyType _bodyType);
    }
}
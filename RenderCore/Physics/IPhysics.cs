using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Tickable;
using Common.VertexObject;

namespace RenderCore.Physics
{
    public interface IPhysics : ITickable
    {
        void SetGravity(Vector2 _gravity);
        IBody CreateVertexBody(IVertexObject _vertexObject, Vector2 _position, float _mass, BodyType _bodyType);
    }
}
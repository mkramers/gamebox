using Common.VertexObject;

namespace RenderCore
{
    public abstract class VertexObjectCreationArgsBase
    {
        protected VertexObjectCreationArgsBase(IVertexObject _vertexObject)
        {
            VertexObject = _vertexObject;
        }

        protected IVertexObject VertexObject { get; }
    }
}
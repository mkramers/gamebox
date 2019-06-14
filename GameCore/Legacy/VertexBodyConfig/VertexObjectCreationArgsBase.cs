using Common.VertexObject;

namespace GameCore.Legacy.VertexBodyConfig
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
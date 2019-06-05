namespace RenderCore
{
    public abstract class VertexObjectCreationArgsBase
    {
        protected VertexObjectCreationArgsBase(IVertexObject _vertexObject)
        {
            VertexObject = _vertexObject;
        }

        public IVertexObject VertexObject { get; }
    }
}
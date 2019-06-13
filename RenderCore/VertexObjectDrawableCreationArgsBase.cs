using Common.VertexObject;

namespace RenderCore
{
    public abstract class VertexObjectDrawableCreationArgsBase : VertexObjectCreationArgsBase, IDrawableCreator
    {
        protected VertexObjectDrawableCreationArgsBase(IVertexObject _vertexObject) : base(_vertexObject)
        {
        }

        public abstract IPositionDrawable CreateDrawable();
    }
}
using Common.VertexObject;
using RenderCore.Drawable;

namespace RenderCore.VertexBodyConfig
{
    public abstract class VertexObjectDrawableCreationArgsBase : VertexObjectCreationArgsBase, IDrawableCreator
    {
        protected VertexObjectDrawableCreationArgsBase(IVertexObject _vertexObject) : base(_vertexObject)
        {
        }

        public abstract IPositionDrawable CreateDrawable();
    }
}
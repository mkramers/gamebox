using SFML.Graphics;

namespace RenderCore
{
    public class VertexObjectShapeCreationArgs : VertexObjectDrawableCreationArgsBase
    {
        public VertexObjectShapeCreationArgs(IVertexObject _vertexObject, Color _fillColor, Color _outlineColor,
            float _outlineThickness) : base(_vertexObject)
        {
            FillColor = _fillColor;
            OutlineColor = _outlineColor;
            OutlineThickness = _outlineThickness;
        }

        private Color FillColor { get; }
        private Color OutlineColor { get; }
        private float OutlineThickness { get; }

        public override IPositionDrawable CreateDrawable()
        {
            ConvexShape shape = ShapeFactory.GetConvexShape(VertexObject);
            shape.OutlineThickness = OutlineThickness;
            shape.FillColor = FillColor;
            shape.OutlineColor = OutlineColor;

            Drawable<ConvexShape> shapeDrawable = new Drawable<ConvexShape>(shape);
            return shapeDrawable;
        }
    }
}
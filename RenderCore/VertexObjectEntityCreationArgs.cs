namespace RenderCore
{
    public class VertexObjectEntityCreationArgs : IEntityCreator
    {
        public VertexObjectEntityCreationArgs(IBodyCreator _bodyCreator, IDrawableCreator _drawableCreator)
        {
            BodyCreator = _bodyCreator;
            DrawableCreator = _drawableCreator;
        }

        public IBodyCreator BodyCreator { get; }
        public IDrawableCreator DrawableCreator { get; }

        public IEntity CreateEntity(IPhysics _physics)
        {
            IBody body = BodyCreator.CreateBody(_physics);
            IPositionDrawable drawable = DrawableCreator.CreateDrawable();

            Entity entity = new Entity(drawable, body);
            return entity;
        }
    }
}
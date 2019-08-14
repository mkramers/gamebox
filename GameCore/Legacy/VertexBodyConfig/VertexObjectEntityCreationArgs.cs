using GameCore.Entity;
using PhysicsCore;
using RenderCore.Drawable;

namespace GameCore.Legacy.VertexBodyConfig
{
    public class VertexObjectEntityCreationArgs : IEntityCreator
    {
        public VertexObjectEntityCreationArgs(IBodyCreator _bodyCreator, IDrawableCreator _drawableCreator)
        {
            BodyCreator = _bodyCreator;
            DrawableCreator = _drawableCreator;
        }

        private IBodyCreator BodyCreator { get; }
        private IDrawableCreator DrawableCreator { get; }

        public IEntity CreateEntity(IPhysics _physics)
        {
            IBody body = BodyCreator.CreateBody(_physics);
            _physics.Add(body);

            IPositionDrawable drawable = DrawableCreator.CreateDrawable();

            Entity.Entity entity = new Entity.Entity(drawable, body);
            return entity;
        }
    }
}
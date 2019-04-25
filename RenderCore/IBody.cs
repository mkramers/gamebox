using SFML.Graphics;

namespace RenderCore
{
    public interface IBody
    {
        IRenderable GetBodyRepresentation();
    }

    public interface IRigidBody : IBody
    {
        void ApplyForce(IForce _force);
    }

    public interface IForce
    {

    }

    public abstract class BodyBase : IBody
    {
        private readonly IRenderable m_renderable;

        protected BodyBase(IRenderable _renderable)
        {
            m_renderable = _renderable;
        }

        public IRenderable GetBodyRepresentation()
        {
            return m_renderable;
        }
    }

    public class RigidBody : BodyBase, IRigidBody
    {
        public RigidBody(IRenderable _renderable) : base(_renderable)
        {
        }

        public void ApplyForce(IForce _force)
        {
        }
    }

    public class ManBodyFactory
    {
        public IBody GetManBody()
        {
            CoreSpriteFactory spriteFactory = new CoreSpriteFactory();
            CoreSprite manSprite = spriteFactory.GetSprite(ResourceId.MAN, new IntRect(50, 24, 24, 24));
            RigidBody body = new RigidBody(manSprite);
            return body;
        }
    }
}
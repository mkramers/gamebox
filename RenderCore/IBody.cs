using SFML.Graphics;

namespace RenderCore
{
    public interface IBody
    {
        IBodyRepresentation GetBodyRepresentation();
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
        private readonly IBodyRepresentation m_bodyRepresentation;

        protected BodyBase(IBodyRepresentation _bodyRepresentation)
        {
            m_bodyRepresentation = _bodyRepresentation;
        }

        public IBodyRepresentation GetBodyRepresentation()
        {
            return m_bodyRepresentation;
        }
    }

    public class RigidBody : BodyBase, IRigidBody
    {
        public RigidBody(IBodyRepresentation _bodyRepresentation) : base(_bodyRepresentation)
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
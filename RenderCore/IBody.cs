using SFML.Graphics;

namespace RenderCore
{
    public interface IBody
    {
        IBodyRepresentation GetBodyRepresentation();
    }

    public class BodyBase : IBody
    {
        private readonly IBodyRepresentation m_bodyRepresentation;

        public BodyBase(IBodyRepresentation _bodyRepresentation)
        {
            m_bodyRepresentation = _bodyRepresentation;
        }

        public IBodyRepresentation GetBodyRepresentation()
        {
            return m_bodyRepresentation;
        }
    }

    public class ManBodyFactory
    {
        public IBody GetManBody()
        {
            CoreSpriteFactory spriteFactory = new CoreSpriteFactory();
            CoreSprite manSprite = spriteFactory.GetSprite(ResourceId.MAN, new IntRect(50, 24, 24, 24));
            BodyBase body = new BodyBase(manSprite);
            return body;
        }
    }
}
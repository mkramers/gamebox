using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public interface IBody
    {
        Drawable GetDrawable();
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
        private readonly Drawable m_drawable;

        protected BodyBase(Drawable _drawable)
        {
            m_drawable = _drawable;
        }

        public Drawable GetDrawable()
        {
            return m_drawable;
        }
    }

    public class RigidBody : BodyBase, IRigidBody
    {
        public RigidBody(Drawable _drawable) : base(_drawable)
        {
        }

        public void ApplyForce(IForce _force)
        {
        }
    }

    public class ManBodyFactory
    {
        public IBody GetManBody(float _tileSize)
        {
            CoreSpriteFactory spriteFactory = new CoreSpriteFactory();
            IntRect textureCrop = new IntRect(50, 24, 24, 24);
            Sprite sprite = spriteFactory.GetSprite(ResourceId.MAN, textureCrop);
            sprite.Scale = new Vector2f(_tileSize * (1.0f / textureCrop.Width), _tileSize * (1.0f / textureCrop.Height));
            RigidBody body = new RigidBody(sprite);
            return body;
        }
    }
}
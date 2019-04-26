using SFML.Graphics;

namespace RenderCore
{
    public class RigidBody : BodyBase, IRigidBody
    {
        public RigidBody(Drawable _drawable) : base(_drawable)
        {
        }

        public void ApplyForce(IForce _force)
        {
        }
    }
}
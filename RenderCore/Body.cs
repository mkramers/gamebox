using Vector2 = System.Numerics.Vector2;

namespace RenderCore
{
    public class Body : IBody
    {
        private readonly Aether.Physics2D.Dynamics.Body m_body;

        public Body(Aether.Physics2D.Dynamics.Body _body)
        {
            m_body = _body;
        }

        public Vector2 GetPosition()
        {
            return m_body.Position.GetVector2();
        }

        public void ApplyForce(NormalForce _force)
        {
            m_body.ApplyLinearImpulse(_force.ForceVector.GetVector2());
        }

        public void RemoveFromWorld()
        {
            var world = m_body.World;
            world.Remove(m_body);
        }
    }
}
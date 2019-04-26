namespace RenderCore
{
    public interface IRigidBody : IBody
    {
        void ApplyForce(IForce _force);
    }
}
namespace PhysicsCore
{
    public interface IBodyCreator
    {
        IBody CreateBody(IPhysics _physics);
    }
}
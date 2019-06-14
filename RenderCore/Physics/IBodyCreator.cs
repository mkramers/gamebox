namespace RenderCore.Physics
{
    public interface IBodyCreator
    {
        IBody CreateBody(IPhysics _physics);
    }
}
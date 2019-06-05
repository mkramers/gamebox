namespace RenderCore
{
    public interface IBodyCreator
    {
        IBody CreateBody(IPhysics _physics);
    }
}
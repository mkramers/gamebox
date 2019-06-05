namespace RenderCore
{
    public interface IEntityCreator
    {
        IEntity CreateEntity(IPhysics _physics);
    }
}
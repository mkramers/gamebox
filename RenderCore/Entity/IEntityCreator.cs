using RenderCore.Physics;

namespace RenderCore.Entity
{
    public interface IEntityCreator
    {
        IEntity CreateEntity(IPhysics _physics);
    }
}
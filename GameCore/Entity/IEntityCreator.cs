using PhysicsCore;

namespace GameCore.Entity
{
    public interface IEntityCreator
    {
        IEntity CreateEntity(IPhysics _physics);
    }
}
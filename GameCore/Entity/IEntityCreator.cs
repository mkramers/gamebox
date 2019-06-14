using RenderCore.Physics;

namespace GameCore.Entity
{
    public interface IEntityCreator
    {
        IEntity CreateEntity(IPhysics _physics);
    }
}
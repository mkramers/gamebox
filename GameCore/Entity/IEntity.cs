using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;

namespace GameCore.Entity
{
    public interface IEntity : IBody, ITickable, IPositionDrawable
    {
    }
}
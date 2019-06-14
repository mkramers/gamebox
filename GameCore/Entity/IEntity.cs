using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.Physics;

namespace GameCore.Entity
{
    public interface IEntity : IBody, ITickable, IPositionDrawable
    {
    }
}
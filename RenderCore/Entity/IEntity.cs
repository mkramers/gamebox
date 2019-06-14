using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.Physics;

namespace RenderCore.Entity
{
    public interface IEntity : IBody, ITickable, IPositionDrawable
    {
    }
}
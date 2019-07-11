using System.Collections.Generic;
using GameCore.Entity;
using PhysicsCore;
using RenderCore.Drawable;

namespace GameCore.Maps
{
    public interface IMap
    {
        IEnumerable<IEntity> GetEntities(IPhysics _physics);
        IEnumerable<IDrawable> GetDrawables();
    }
}
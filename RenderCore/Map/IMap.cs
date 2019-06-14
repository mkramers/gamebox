using System.Collections.Generic;
using RenderCore.Entity;
using RenderCore.Physics;

namespace RenderCore.Map
{
    public interface IMap
    {
        IEnumerable<IEntity> GetEntities(IPhysics _physics);
    }
}
using System.Collections.Generic;
using GameCore.Entity;
using RenderCore.Physics;

namespace GameCore.Map
{
    public interface IMap
    {
        IEnumerable<IEntity> GetEntities(IPhysics _physics);
    }
}
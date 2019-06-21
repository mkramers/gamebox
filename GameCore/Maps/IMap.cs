using System.Collections.Generic;
using GameCore.Entity;
using PhysicsCore;

namespace GameCore.Maps
{
    public interface IMap
    {
        IEnumerable<IEntity> GetEntities(IPhysics _physics);
    }
}
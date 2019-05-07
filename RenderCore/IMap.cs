using System.Collections.Generic;

namespace RenderCore
{
    public interface IMap
    {
        IEnumerable<IEntity> GetEntities(IPhysics _physics);
    }
}
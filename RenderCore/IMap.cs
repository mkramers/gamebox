using System.Collections.Generic;
using System.Numerics;

namespace RenderCore
{
    public interface IMap
    {
        IEnumerable<IEntity> GetEntities(IPhysics _physics, Vector2 _position);
    }
}
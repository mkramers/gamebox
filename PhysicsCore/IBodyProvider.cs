using System.Collections.Generic;

namespace PhysicsCore
{
    public interface IBodyProvider
    {
        IEnumerable<IBody> GetBodies();
    }
}
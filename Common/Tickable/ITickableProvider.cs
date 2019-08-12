using System.Collections.Generic;

namespace Common.Tickable
{
    public interface ITickableProvider
    {
        IEnumerable<ITickable> GetTickables();
    }
}
using System.Collections.Generic;

namespace Common.Tickable
{
    public class TickableProvider : ITickableProvider
    {
        private readonly ITickable m_tickable;

        public TickableProvider(ITickable _tickable)
        {
            m_tickable = _tickable;
        }

        public virtual IEnumerable<ITickable> GetTickables()
        {
            return new[] {m_tickable};
        }
    }
}
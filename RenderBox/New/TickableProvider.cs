using System.Collections.Generic;
using Common.Tickable;

namespace RenderBox.New
{
    public class TickableProvider : ITickableProvider
    {
        private readonly ITickable m_tickable;

        public TickableProvider(ITickable _tickable)
        {
            m_tickable = _tickable;
        }

        public IEnumerable<ITickable> GetTickables()
        {
            return new[] {m_tickable};
        }
    }
}
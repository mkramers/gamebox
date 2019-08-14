using System.Collections.Generic;

namespace PhysicsCore
{
    public class BodyProvider : IBodyProvider
    {
        private readonly IBody m_body;

        public BodyProvider(IBody _body)
        {
            m_body = _body;
        }

        public IEnumerable<IBody> GetBodies()
        {
            return new[] {m_body};
        }
    }
}
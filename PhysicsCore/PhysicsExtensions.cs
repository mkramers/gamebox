using System.Collections.Generic;
using System.Linq;

namespace PhysicsCore
{
    public static class PhysicsExtensions
    {
        public static void UpdateCurrentBodies(this IPhysics _physics, IEnumerable<IBody> _bodies)
        {
            List<IBody> currentBodies = _physics.GetBodies().ToList();

            IEnumerable<IBody> bodies = _bodies as IBody[] ?? _bodies.ToArray();

            foreach (IBody body in bodies)
            {
                if (!currentBodies.Contains(body))
                {
                    _physics.Add(body);
                }
            }

            foreach (IBody currentBody in currentBodies)
            {
                if (!bodies.Contains(currentBody) && _physics.ContainsBody(currentBody))
                {
                    _physics.Remove(currentBody);
                }
            }
        }
    }
}
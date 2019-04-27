using System.Diagnostics;
using System.Numerics;

namespace RenderCore
{
    public class NormalForce : IForce
    {
        public Vector2 ForceVector { get; private set; }

        public NormalForce(Vector2 _forceVector)
        {
            ForceVector = _forceVector;
        }

        public void Add(IForce _force)
        {
            NormalForce normalForce = _force as NormalForce;
            Debug.Assert(normalForce != null);

            ForceVector += normalForce.ForceVector;
        }

        public void Subtract(IForce _force)
        {
            NormalForce normalForce = _force as NormalForce;
            Debug.Assert(normalForce != null);

            ForceVector -= normalForce.ForceVector;
        }
    }
}
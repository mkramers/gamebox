using System.Numerics;
using System.Runtime.CompilerServices;
using BepuPhysics;

namespace RenderCore.Physics
{
    public struct PoseIntegratorCallbacks : IPoseIntegratorCallbacks
    {
        private readonly Vector3 m_gravity;
        private Vector3 m_gravityDt;

        public PoseIntegratorCallbacks(Vector3 _gravity) : this()
        {
            m_gravity = _gravity;
        }

        public void PrepareForIntegration(float _dt)
        {
            m_gravityDt = m_gravity * _dt;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IntegrateVelocity(int _bodyIndex, in RigidPose _pose, in BodyInertia _localInertia,
            int _workerIndex,
            ref BodyVelocity _velocity)
        {
            //Note that we avoid accelerating kinematics. Kinematics are any body with an inverse mass of zero (so a mass of ~infinity). No force can move them.
            if (_localInertia.InverseMass > 0)
            {
                _velocity.Linear += m_gravityDt;
            }
        }

        public AngularIntegrationMode AngularIntegrationMode => AngularIntegrationMode.ConserveMomentum;
    }
}
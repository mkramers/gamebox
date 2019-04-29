using System.Runtime.CompilerServices;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.CollisionDetection;
using BepuPhysics.Constraints;

namespace RenderCore.Physics
{
    public unsafe struct NarrowPhaseCallbacks : INarrowPhaseCallbacks
    {
        public void Initialize(Simulation _simulation)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AllowContactGeneration(int _workerIndex, CollidableReference _a, CollidableReference _b)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ConfigureContactManifold(int _workerIndex, CollidablePair _pair, ConvexContactManifold* _manifold,
            out PairMaterialProperties _pairMaterial)
        {
            ConfigureMaterial(out _pairMaterial);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ConfigureContactManifold(int _workerIndex, CollidablePair _pair,
            NonconvexContactManifold* _manifold,
            out PairMaterialProperties _pairMaterial)
        {
            ConfigureMaterial(out _pairMaterial);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ConfigureMaterial(out PairMaterialProperties pairMaterial)
        {
            //The engine does not define any per-body material properties. Instead, all material lookup and blending operations are handled by the callbacks.
            //For the purposes of this demo, we'll use the same settings for all pairs.
            //(Note that there's no bounciness property! See here for more details: https://github.com/bepu/bepuphysics2/issues/3)
            pairMaterial.FrictionCoefficient = 1f;
            pairMaterial.MaximumRecoveryVelocity = 2f;
            pairMaterial.SpringSettings = new SpringSettings(30, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AllowContactGeneration(int _workerIndex, CollidablePair _pair, int _childIndexA, int _childIndexB)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ConfigureContactManifold(int _workerIndex, CollidablePair _pair, int _childIndexA, int _childIndexB,
            ConvexContactManifold* _manifold)
        {
            return true;
        }

        public void Dispose()
        {
        }
    }
}
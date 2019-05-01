using System;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities.Memory;
using RenderCore;
using RenderCore.Physics;

namespace BepuSample
{
    internal class Program
    {
        private static void Main(string[] _args)
        {
            BufferPool bufferPool = new BufferPool();

            Physics2 physics = new Physics2(bufferPool);

            IDynamicBody tc = physics.CreateDynamicBody(1.0f);
            IStaticBody staticBody = physics.CreateStaticBody(5 * Vector3.UnitY);

            for (int i = 0; i < 100; ++i)
            {
                physics.Tick(1);

                Vector3 bd = tc.GetPosition();
                Vector3 lp = staticBody.GetPosition();
                Console.WriteLine(bd + "\t\t" + lp);
            }

            tc.Dispose();

            physics.Dispose();
            bufferPool.Clear();
        }
    }
}
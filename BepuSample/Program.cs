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

            IPhysicalBody tc = physics.CreatePhysicalObject();
            IStaticBody staticBody = physics.CreateStaticObject();

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
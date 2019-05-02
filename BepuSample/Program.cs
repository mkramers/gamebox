using System;
using System.Numerics;
using RenderCore;

namespace BepuSample
{
    internal class Program
    {
        private static void Main(string[] _args)
        {
            Physics();
        }

        private static void Physics()
        {
            Physics2 physics = new Physics2();

            IBody tc = physics.CreateDynamicBody(1.0f);
            IBody staticBody = physics.CreateStaticBody(-5 * Vector2.UnitY, 1);

            for (int i = 0; i < 100; ++i)
            {
                physics.Tick(1);

                var bd = tc.GetPosition();
                var lp = staticBody.GetPosition();
                Console.WriteLine(bd + "\t\t" + lp);
            }

            physics.Dispose();
        }
    }
}
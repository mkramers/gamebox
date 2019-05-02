using System;
using System.Numerics;
using Aether.Physics2D.Dynamics;
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
            Vector2 gravity = new Vector2(0, -10);

            Physics2 physics = new Physics2(gravity);

            IBody dynamicBody = physics.CreateBody(5 * Vector2.UnitY, 1.0f, BodyType.Dynamic);
            IBody staticBody = physics.CreateBody(-5 * Vector2.UnitY, 1.0f, BodyType.Static);

            for (int i = 0; i < 1000; ++i)
            {
                physics.Tick(TimeSpan.FromMilliseconds(30));

                Vector2 bd = dynamicBody.GetPosition();
                Vector2 lp = staticBody.GetPosition();
                Console.WriteLine(bd + "\t\t" + lp);
            }

            physics.Dispose();
        }
    }
}
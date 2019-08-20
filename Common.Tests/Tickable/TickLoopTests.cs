using System;
using Common.Tickable;
using NUnit.Framework;

namespace Common.Tests.Tickable
{
    [TestFixture]
    public class TickLoopTests
    {
        [Test]
        public void TicksWithCorrectElapsed()
        {
            TimeSpan tickPeriod = TimeSpan.FromMilliseconds(1);

            TickLoop tickLoop = new TickLoop(tickPeriod);
            tickLoop.Tick += TickLoopOnTick;

            TimeSpan actualElapsed = TimeSpan.Zero;

            void TickLoopOnTick(object _sender, TimeElapsedEventArgs _e)
            {
                tickLoop.StopLoop();
                actualElapsed = _e.Elapsed;
            }
 
            tickLoop.StartLoop();

            Assert.That(actualElapsed, Is.EqualTo(tickPeriod).Within(TimeSpan.FromMilliseconds(1)));
        }
    }
}
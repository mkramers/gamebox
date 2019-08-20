using System;
using System.Diagnostics;
using Common.Extensions;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class StopwatchExtensionsTests
    {
        [Test]
        public void GetsElapsedAndRestarts()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Stop();

            TimeSpan expectedElapsed = stopwatch.Elapsed;
            TimeSpan actualElapsed = stopwatch.GetElapsedAndRestart();

            Assert.Multiple(() =>
            {
                Assert.That(expectedElapsed, Is.EqualTo(actualElapsed));
                Assert.That(stopwatch.IsRunning, Is.True, "Stopwatch not running!");
            });
        }
    }
}
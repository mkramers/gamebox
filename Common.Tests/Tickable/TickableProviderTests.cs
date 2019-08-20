using System.Collections.Generic;
using Common.Tickable;
using NUnit.Framework;

namespace Common.Tests.Tickable
{
    [TestFixture]
    public class TickableProviderTests
    {
        [Test]
        public void ReturnsCorrectTickables()
        {
            MockTickable mockTickable = new MockTickable();
         
            TickableProvider mock = new TickableProvider(mockTickable);
            MockTickable[] expectedTickables = {mockTickable};

            IEnumerable<ITickable> actualTickables = mock.GetTickables();
            Assert.That(expectedTickables, Is.EquivalentTo(actualTickables));
        }
    }
}
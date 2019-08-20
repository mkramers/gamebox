using System;
using System.Collections.Concurrent;
using Common.Containers;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class BlockingCollectionExtensionsTests
    {
        [Test]
        public void ClearsCorrectly()
        {
            BlockingCollection<int> collection = new BlockingCollection<int>
            {
                0, 1, 2, 3
            };

            collection.Clear();

            Assert.Multiple(() =>
            {
                Assert.That(collection.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void ThrowsWhenNull()
        {
            BlockingCollection<int> collection = null;

            Assert.Throws<ArgumentNullException>(() => collection.Clear());
        }
    }
}
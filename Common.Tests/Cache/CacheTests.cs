using System;
using Common.Cache;
using NUnit.Framework;

namespace Common.Tests.Cache
{
    [TestFixture]
    public class CacheTests
    {
        [Test]
        public void AddedObjectsExist()
        {
            const int numberOfObjects = 10;

            Cache<string, int> cache = new Cache<string, int>();
            for (int i = 0; i < numberOfObjects; i++)
            {
                cache.AddObject(i, i.ToString());
            }

            Assert.Multiple(() =>
            {
                for (int i = 0; i < numberOfObjects; i++)
                {
                    Assert.That(cache.EntryExists(i), Is.True, $"Entry with id {i} does not exist!");
                }
            });
        }

        [Test]
        public void AddedObjectsHasCorrectValue()
        {
            const int numberOfObjects = 10;

            Cache<string, int> cache = new Cache<string, int>();
            for (int i = 0; i < numberOfObjects; i++)
            {
                cache.AddObject(i, i.ToString());
            }

            Assert.Multiple(() =>
            {
                for (int i = 0; i < numberOfObjects; i++)
                {
                    string value = cache.GetObject(i);
                    Assert.That(value, Is.EqualTo(i.ToString()), $"Value at id {i} not correct");
                }
            });
        }

        [Test]
        public void ThrowsWhenObjectNotInCache()
        {
            Cache<string, int> cache = new Cache<string, int>();
            cache.AddObject(0, "value");

            Assert.Throws<Exception>(() => cache.GetObject(1));
        }

        [Test]
        public void ThrowsWhenAddingExistingObject()
        {
            Cache<string, int> cache = new Cache<string, int>();
            cache.AddObject(0, "value");

            Assert.Throws<Exception>(() => cache.AddObject(0, "new value"));
        }
    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Common.Cache;
using Common.Containers;
using Common.Geometry;
using NUnit.Framework;

namespace Common.Tests
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

    [TestFixture]
    public class GridTests
    {
        [Test]
        public void ThrowsWhenIncorrectRowsAndColumns()
        {
            List<int> cells = new List<int> { 0 };
            Assert.Throws<InvalidDataException>(() => new Grid.Grid<int>(cells, 5, 5));
        }
    }

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

    [TestFixture]
    public class LineSegmentTests
    {
        [Test]
        public void EndIsCorrect()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(1, 1);

            LineSegment lineSegment = new LineSegment(start, end);

            Assert.That(end, Is.EqualTo(lineSegment.End));
        }

        [Test]
        public void StartIsCorrect()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(1, 1);

            LineSegment lineSegment = new LineSegment(start, end);

            Assert.That(start, Is.EqualTo(lineSegment.Start));
        }

        [Test]
        public void LengthIsCorrect()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(1, 0);

            float length = (end - start).Length();

            LineSegment lineSegment = new LineSegment(start, end);

            Assert.That(length, Is.EqualTo(lineSegment.Length));
        }

        [Test]
        public void DirectionIsCorrect()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(1, 0);

            Vector2 direction = Vector2.Normalize(end - start);

            LineSegment lineSegment = new LineSegment(start, end);

            Assert.That(direction, Is.EqualTo(lineSegment.Direction));
        }
    }

    [TestFixture]
    public class Vector2ExtensionsTests
    {
        [Test]
        public void DisplayStringNotNullOrWhiteSpace()
        {
            Vector2 vector = new Vector2(0, 0);
            string displayString = vector.GetDisplayString();

            Assert.That(!string.IsNullOrWhiteSpace(displayString), Is.True, "Display string null/white space");
        }

        [Test]
        public void DisplayStringsCorrect()
        {
            const int numberOfVectors = 5;

            Vector2 vector = new Vector2(0, 0);
            string displayString = vector.GetDisplayString();

            IEnumerable<Vector2> vectors = Enumerable.Repeat(vector, numberOfVectors);
            string combinedDisplayString =
                string.Join(Environment.NewLine, vectors.Select(_vector => _vector.GetDisplayString()));

            string actualDisplayString = vectors.GetDisplayString();

            Assert.That(combinedDisplayString, Is.EqualTo(actualDisplayString));
        }
    }

    [TestFixture]
    public class LineSegmentExtensionsTests
    {
        [Test]
        public void FlipsCorrectly()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(1, 0);
            LineSegment lineSegment = new LineSegment(start, end);

            LineSegment flipped = lineSegment.GetFlipped();
            Assert.Multiple(() =>
            {
                Assert.That(flipped.Start, Is.EqualTo(end));
                Assert.That(flipped.End, Is.EqualTo(start));
            });
        }
    }
}
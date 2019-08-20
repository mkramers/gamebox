using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Common.Cache;
using Common.Containers;
using Common.Extensions;
using Common.Geometry;
using Common.ReflectionUtilities;
using Common.Tickable;
using Common.VertexObject;
using Moq;
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

    [TestFixture]
    public class VertexObjectExtensionsTests
    {
        [Test]
        public void TranslatesCorrectly()
        {
            Vector2 translation = new Vector2(1, 1);

            Vector2 vector = new Vector2(0, 0);
            Polygon polygon = new Polygon { vector };

            IVertexObject translated = polygon.Translate(translation);
            Assert.That(translated.First(), Is.EqualTo(translation));
        }
    }

    [TestFixture]
    public class IntRectTests
    {
        [Test]
        public void ConstructsCorrectly()
        {
            const int x = 0;
            const int y = 2;
            const int width = 3;
            const int height = 4;
            IntRect intRect = new IntRect(x, y, width, height);

            Assert.Multiple(() =>
            {
                Assert.That(intRect.X, Is.EqualTo(x));
                Assert.That(intRect.Y, Is.EqualTo(y));
                Assert.That(intRect.Width, Is.EqualTo(width));
                Assert.That(intRect.Height, Is.EqualTo(height));
            });
        }
    }

    [TestFixture]
    public class IntSizeTests
    {
        [Test]
        public void ConstructsCorrectly()
        {
            const int width = 0;
            const int height = 1;
            IntSize intSize = new IntSize(width, height);

            Assert.Multiple(() =>
            {
                Assert.That(intSize.Width, Is.EqualTo(width));
                Assert.That(intSize.Height, Is.EqualTo(height));
            });
        }
    }

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

    [TestFixture]
    public class FloatExtensionsTests
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(0.1f, 5.72957802f);
                yield return new TestCaseData(-10.1f, 141.312622f);
                yield return new TestCaseData(2.66f, 152.406769f);
                yield return new TestCaseData(8.1f, 104.095825f);
            }
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void DegreesCorrect(float _radians, float _expectedDegrees)
        {
            float actualDegrees = _radians.ToDegrees();

            Debug.WriteLine(actualDegrees);

            Assert.That(actualDegrees, Is.EqualTo(_expectedDegrees).Within(0.00001f));
        }
    }

    [TestFixture]
    public class ReflectionUtilitiesTests
    {
        public class TestTypeBase
        { }

        public class TestType : TestTypeBase
        { }

        public class OtherTestType
        { }

        [Test]
        public void FindsAllDerivedTypesCorrectlyGivenAssembly()
        {
            Type[] expectedTypes = {
                typeof(TestType),
            };

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = currentAssembly.FindAllDerivedTypes<TestTypeBase>();

            Assert.That(expectedTypes, Is.EquivalentTo(types));
        }
        [Test]
        public void FindsAllDerivedTypesCorrectly()
        {
            Type[] expectedTypes = {
                typeof(TestType),
            };

            IEnumerable<Type> types =
                ReflectionUtilities.ReflectionUtilities.FindAllDerivedTypes<TestTypeBase>();

            Assert.That(expectedTypes, Is.EquivalentTo(types));
        }
    }

    public class MockTickable : ITickable
    {
        public bool Ticked { get; private set; }
        public void Tick(TimeSpan _elapsed)
        {
            Ticked = true;
        }
    }

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
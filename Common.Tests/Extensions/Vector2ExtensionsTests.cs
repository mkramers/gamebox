using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common.Extensions;
using Common.Geometry;
using NUnit.Framework;

namespace Common.Tests.Extensions
{
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
}
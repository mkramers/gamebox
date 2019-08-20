using Common.Geometry;
using NUnit.Framework;

namespace Common.Tests.Geometry
{
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
}
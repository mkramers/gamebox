using Common.Geometry;
using NUnit.Framework;

namespace Common.Tests.Geometry
{
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
}
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Common.Tests
{
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
}
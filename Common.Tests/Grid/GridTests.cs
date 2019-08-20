using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Common.Tests.Grid
{
    [TestFixture]
    public class GridTests
    {
        [Test]
        public void ThrowsWhenIncorrectRowsAndColumns()
        {
            List<int> cells = new List<int> { 0 };
            Assert.Throws<InvalidDataException>(() => new Common.Grid.Grid<int>(cells, 5, 5));
        }
    }
}
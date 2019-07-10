using System.Diagnostics;
using Common.Grid;
using NUnit.Framework;

namespace MarchingSquares.Tests
{
    [TestFixture]
    public class BinaryMaskCreatorTests
    {
        [Test]
        public void ThresholdsCorrectly()
        {
            const int halfSize = 2;
            const int threshold = 0;

            int ValueGenerator(int _x, int _y) => _x < halfSize ? threshold : threshold + 1;

            Grid<int> grid = GridTestUtilities.CreateGrid(halfSize, ValueGenerator);

            Grid<bool> binaryMask = BinaryMaskCreator.CreateBinaryMask(grid, threshold);

            Assert.Multiple(() =>
            {
                for (int y = 0; y < binaryMask.Columns; y++)
                {
                    for (int x = 0; x < binaryMask.Rows; x++)
                    {   
                        Assert.That(binaryMask[x,y] == ValueGenerator(x, y) <= threshold);
                    }
                }
            });
        }
    }
}
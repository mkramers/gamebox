using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Common.Grid;
using NUnit.Framework;

namespace MarchingSquares.Tests
{
    [TestFixture]
    public class MarchingSquaresClassifierTests
    {
        private static IEnumerable TestCases
        {
            // ReSharper disable once UnusedMember.Local
            get
            {
                TestCaseData GetTestCaseData(IReadOnlyList<bool> _grid, IReadOnlyList<byte> _classifiedGrid)
                {
                    Grid<bool> grid = GridTestUtilities.CreateSquareGridFromArray(_grid);

                    Grid<byte> classifiedGrid = GridTestUtilities.CreateSquareGridFromArray(_classifiedGrid);

                    return new TestCaseData(grid, classifiedGrid);
                }

                {
                    bool[] gridValues =
                    {
                        false, false, false, false,
                        false, true, true, false,
                        false, true, true, false,
                        false, false, false, false
                    };
                    byte[] classifiedValues =
                        {2, 3, 1, 6, 15, 9, 4, 12, 8};
                    yield return GetTestCaseData(gridValues, classifiedValues);
                }
                {
                    bool[] gridValues =
                    {
                        false, false, false, false,
                        false, true, true, false,
                        false, true, false, true,
                        false, false, false, false
                    };
                    byte[] classifiedValues =
                        {2, 3, 1, 6, 13, 10, 4, 8, 4};
                    yield return GetTestCaseData(gridValues, classifiedValues);
                }
                {
                    bool[] gridValues =
                    {
                        true, true, true, true,
                        true, false, false, true,
                        true, false, false, true,
                        true, true, true, true
                    };
                    byte[] classifiedValues =
                        {13, 12, 14, 9, 0, 6, 11, 3, 7};
                    yield return GetTestCaseData(gridValues, classifiedValues);
                }
                {
                    bool[] gridValues =
                    {
                        false, false, false, false,
                        false, true, true, false,
                        false, false, false, false,
                        false, false, false, false
                    };
                    byte[] classifiedValues =
                        {2, 3, 1, 4, 12, 8, 0, 0, 0};
                    yield return GetTestCaseData(gridValues, classifiedValues);
                }
            }
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ClassifiesCorrectly(Grid<bool> _binaryMask, Grid<byte> _expectedClassifiedGrid)
        {
            Grid<byte> classifiedGrid = MarchingSquaresClassifier.ClassifyCells(_binaryMask);
            classifiedGrid.PrintGrid("");

            Debug.WriteLine($"Classification result\t\t{string.Join(", ", classifiedGrid)}");
            Debug.WriteLine($"Classification expected\t\t{string.Join(", ", _expectedClassifiedGrid)}");

            Assert.That(classifiedGrid, Is.EquivalentTo(_expectedClassifiedGrid));
        }
    }
}
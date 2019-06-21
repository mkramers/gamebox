using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common.Grid;
using Common.VertexObject;
using NUnit.Framework;

namespace MarchingSquares.Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            const int halfSize = 2;

            IList<GridCell<int>> gridCells = new List<GridCell<int>>();
            for (int x = 0; x < halfSize * 2; x++)
            {
                for (int y = 0; y < halfSize * 2; y++)
                {
                    int value = x < halfSize ? 1 : 0;
                    gridCells.Add(new GridCell<int>(x, y, value));
                }
            }
            Grid<int> grid = new Grid<int>(gridCells);

            //MarchingSquaresGenerator<int> generator = new MarchingSquaresGenerator<int>(grid, threshold);

            //IEnumerable<IVertexObject> vertexObjects = generator.Generate();
        }

        private static IEnumerable ClassifierTestCases
        {
            get
            {
                TestCaseData GetTestCaseData(IReadOnlyList<bool> _grid, IReadOnlyList<byte> _classifiedGrid)
                {
                    Grid<bool> grid = CreateSquareGridFromArray(_grid);

                    Grid<byte> classifiedGrid = CreateSquareGridFromArray(_classifiedGrid);

                    return new TestCaseData(grid, classifiedGrid);
                }

                {
                    bool[] gridValues =
                    {
                        false, false, false, false,
                        false, true, true, false,
                        false, true, true, false,
                        false, false, false, false,
                    };
                    byte[] classifiedValues = { 2, 3, 1, 6, 15, 9, 4, 12, 8 };
                    yield return GetTestCaseData(gridValues, classifiedValues).Returns(true);
                }
                {
                    bool[] gridValues =
                    {
                        false, false, false, false,
                        false, true, true, false,
                        false, true, false, true,
                        false, false, false, false,
                    };
                    byte[] classifiedValues = { 2, 3, 1, 6, 13, 10, 4, 8, 4 };
                    yield return GetTestCaseData(gridValues, classifiedValues).Returns(true);
                }
                {
                    bool[] gridValues =
                    {
                        true, true, true, true,
                        true, false, false, true,
                        true, false, false, true,
                        true, true, true, true,
                    };
                    byte[] classifiedValues = { 13, 12, 14, 9, 0, 6, 11, 3, 7 };
                    yield return GetTestCaseData(gridValues, classifiedValues).Returns(true);
                }
                {
                    bool[] gridValues =
                    {
                        false, false, false, false,
                        false, true, true, false,
                        false, false, false, false,
                        false, false, false, false,
                    };
                    byte[] classifiedValues = { 2, 3, 1, 4, 12, 8, 0, 0, 0 };
                    yield return GetTestCaseData(gridValues, classifiedValues).Returns(true);
                }
            }
        }
        
        [Test, TestCaseSource(nameof(ClassifierTestCases))]
        public bool MarchingSquaresClassifiesCorrectly(Grid<bool> _binaryMask, Grid<byte> _expectedClassifiedGrid)
        {
            Grid<byte> classifiedGrid = MarchingSquaresClassifier.ClassifyCells(_binaryMask);

            Debug.WriteLine($"Classification result {string.Join(", ", classifiedGrid.Select(_cell => _cell.Value))}");

            bool isEqual = classifiedGrid.SequenceEqual(_expectedClassifiedGrid);

            return isEqual;
        }

        private static Grid<T> CreateGrid<T>(int _halfSize, Func<int, int, T> _valueGenerator) where T : IComparable
        {
            IList<GridCell<T>> gridCells = new List<GridCell<T>>();
            for (int x = 0; x < _halfSize * 2; x++)
            {
                for (int y = 0; y < _halfSize * 2; y++)
                {
                    T value = _valueGenerator(x, y);
                    gridCells.Add(new GridCell<T>(x, y, value));
                }
            }

            Grid<T> grid = new Grid<T>(gridCells);
            return grid;
        }

        private static Grid<T> CreateSquareGridFromArray<T>(IReadOnlyList<T> _values) where T : IComparable
        {
            if (Math.Abs(Math.Sqrt(_values.Count / 2.0f) % 1) < 0.000001f)
            {
                throw new Exception("incorrect size of array");
            }

            int sideLength = (int)Math.Sqrt(_values.Count);

            IList<GridCell<T>> gridCells = new List<GridCell<T>>();
            for (int y = 0; y < sideLength; y++)
            {
                for (int x = 0; x < sideLength; x++)
                {
                    T value = _values[y * sideLength + x];
                    gridCells.Add(new GridCell<T>(x, y, value));
                }
            }

            Grid<T> grid = new Grid<T>(gridCells);
            return grid;
        }

        [Test]
        public void BinaryMaskHasCorrectValues()
        {
            const int halfSize = 2;
            const int threshold = 0;

            int ValueGenerator(int _x, int _y) => _x < halfSize ? threshold : threshold + 1;

            Grid<int> grid = CreateGrid(halfSize, ValueGenerator);

            Grid<bool> binaryMask = BinaryMaskCreator.CreateBinaryMask(grid, threshold);

            Assert.Multiple(() =>
            {
                foreach (GridCell<bool> gridCell in binaryMask)
                {
                    Assert.That(gridCell.Value == ValueGenerator(gridCell.X, gridCell.Y) <= threshold);
                }
            });
        }
    }
}
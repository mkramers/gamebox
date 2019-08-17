using System.Collections;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using NUnit.Framework;

namespace IOUtilities.Tests
{
    public enum TestEnum
    {
        A_B,
        A_B_C,
        A_B_C_D,
    }

    [TestFixture]
    public class PathFromEnumTests
    {
        private static IEnumerable TestCases
        {
            get
            {
                MockFileSystem fileSystem = new MockFileSystem();
                const string extension = "ext";
                yield return new TestCaseData(TestEnum.A_B, extension, fileSystem.Path.Combine("a", $"a-b.{extension}"));
                yield return new TestCaseData(TestEnum.A_B_C, extension, fileSystem.Path.Combine("a", "b", $"b-c.{extension}"));
                yield return new TestCaseData(TestEnum.A_B_C_D, extension, fileSystem.Path.Combine("a", "b", "c", $"c-d.{extension}"));
            }
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void EnumToPathIsCorrect(TestEnum _enum, string _extension, string _expectedPath)
        {
            TestEnum actualEnum = EnumFromPath.GetEnumFromPath<TestEnum>(_expectedPath);

            Assert.That(actualEnum, Is.EqualTo(_enum));
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void GetPathFromEnumIsCorrect(TestEnum  _enum, string _extension, string _expectedPath)
        {
            string pathFromEnum = PathFromEnum<TestEnum>.GetPathFromEnum(_enum, _extension);

            Assert.That(pathFromEnum, Is.EqualTo(_expectedPath));
        }
    }
}
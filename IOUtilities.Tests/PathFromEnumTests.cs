using System.Collections;
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

    internal static class EnumTestCases
    {
        public static MockFileSystem FileSystem { get; } = new MockFileSystem();

        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(TestEnum.A_B, FileSystem.Path.Combine("a", "a-b"));
                yield return new TestCaseData(TestEnum.A_B_C, FileSystem.Path.Combine("a", "b", "b-c"));
                yield return new TestCaseData(TestEnum.A_B_C_D, FileSystem.Path.Combine("a", "b", "c", "c-d"));
            }
        }
    }

    [TestFixture]
    public class EnumToPathTests
    {
        [Test, TestCaseSource(typeof(EnumTestCases), nameof(EnumTestCases.TestCases))]
        public void EnumToPathIsCorrect(TestEnum _enum, string _expectedPath)
        {
            EnumFromPath enumFromPath = new EnumFromPath(EnumTestCases.FileSystem);
            TestEnum actualEnum = enumFromPath.GetEnumFromPath<TestEnum>(_expectedPath);

            Assert.That(actualEnum, Is.EqualTo(_enum));
        }
    }

    [TestFixture]
    public class PathFromEnumTests
    {
        [Test, TestCaseSource(typeof(EnumTestCases), nameof(EnumTestCases.TestCases))]
        public void GetPathFromEnumIsCorrect(TestEnum  _enum, string _expectedPath)
        {
            PathFromEnum<TestEnum> pathFromEnum = new PathFromEnum<TestEnum>();
            string path = pathFromEnum.GetPathFromEnum(_enum);

            Assert.That(path, Is.EqualTo(_expectedPath));
        }
    }
}
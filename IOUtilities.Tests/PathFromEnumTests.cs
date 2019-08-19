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

    [TestFixture]
    public class PathFromEnumTests
    {
        private static readonly MockFileSystem m_fileSystem = new MockFileSystem();

        private static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(TestEnum.A_B, m_fileSystem.Path.Combine("a", "a-b"));
                yield return new TestCaseData(TestEnum.A_B_C, m_fileSystem.Path.Combine("a", "b", "b-c"));
                yield return new TestCaseData(TestEnum.A_B_C_D, m_fileSystem.Path.Combine("a", "b", "c", "c-d"));
            }
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void EnumToPathIsCorrect(TestEnum _enum, string _expectedPath)
        {
            EnumFromPath enumFromPath = new EnumFromPath(m_fileSystem);
            TestEnum actualEnum = enumFromPath.GetEnumFromPath<TestEnum>(_expectedPath);

            Assert.That(actualEnum, Is.EqualTo(_enum));
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void GetPathFromEnumIsCorrect(TestEnum  _enum, string _expectedPath)
        {
            PathFromEnum<TestEnum> pathFromEnum = new PathFromEnum<TestEnum>();
            string path = pathFromEnum.GetPathFromEnum(_enum);

            Assert.That(path, Is.EqualTo(_expectedPath));
        }
    }
}
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
                const string extension = "ext";
                yield return new TestCaseData(TestEnum.A_B, extension, m_fileSystem.Path.Combine("a", $"a-b.{extension}"));
                yield return new TestCaseData(TestEnum.A_B_C, extension, m_fileSystem.Path.Combine("a", "b", $"b-c.{extension}"));
                yield return new TestCaseData(TestEnum.A_B_C_D, extension, m_fileSystem.Path.Combine("a", "b", "c", $"c-d.{extension}"));
            }
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void EnumToPathIsCorrect(TestEnum _enum, string _, string _expectedPath)
        {
            EnumFromPath enumFromPath = new EnumFromPath(m_fileSystem);
            TestEnum actualEnum = enumFromPath.GetEnumFromPath<TestEnum>(_expectedPath);

            Assert.That(actualEnum, Is.EqualTo(_enum));
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void GetPathFromEnumIsCorrect(TestEnum  _enum, string _extension, string _expectedPath)
        {
            PathFromEnum<TestEnum> pathFromEnum = new PathFromEnum<TestEnum>(m_fileSystem);
            string path = pathFromEnum.GetPathFromEnum(_enum, _extension);

            Assert.That(path, Is.EqualTo(_expectedPath));
        }
    }
}
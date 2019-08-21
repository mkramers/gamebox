using System;
using System.Collections;
using System.Diagnostics;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Text.RegularExpressions;
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
        public static IPath FileSystemPath { get; } = new MockPath(new MockFileSystem());

        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(TestEnum.A_B, FileSystemPath.Combine("a", "a-b"));
                yield return new TestCaseData(TestEnum.A_B_C, FileSystemPath.Combine("a", "b", "b-c"));
                yield return new TestCaseData(TestEnum.A_B_C_D, FileSystemPath.Combine("a", "b", "c", "c-d"));
            }
        }
    }

    [TestFixture]
    public class EnumToPathTests
    {
        [Test, TestCaseSource(typeof(EnumTestCases), nameof(EnumTestCases.TestCases))]
        public void EnumToPathIsCorrect(TestEnum _enum, string _expectedPath)
        {
            EnumFromPath enumFromPath = new EnumFromPath(EnumTestCases.FileSystemPath);
            TestEnum actualEnum = enumFromPath.GetEnumFromPath<TestEnum>(_expectedPath);

            Assert.That(actualEnum, Is.EqualTo(_enum));
        }

        [Test]
        public void ConstructorDoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                EnumFromPath a = new EnumFromPath();
            });
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

    [TestFixture]
    public class PathFromEnumPathConverterTests
    {
        [Test, TestCaseSource(typeof(EnumTestCases), nameof(EnumTestCases.TestCases))]
        public void PathConvertsCorrectly(TestEnum _enum, string _expectedPath)
        {
            const string rootDirectory = "test";
            const string fileExtension = ".txt";

            IPath fileSystemPath = EnumTestCases.FileSystemPath;

            PathFromEnumPathConverter<TestEnum> converter = new PathFromEnumPathConverter<TestEnum>(rootDirectory, fileExtension, fileSystemPath);
            string path = converter.GetPath(_enum);

            string expectedFullPath = fileSystemPath.Combine(rootDirectory, fileSystemPath.ChangeExtension(_expectedPath, fileExtension));

            Assert.That(path, Is.EqualTo(expectedFullPath));
        }
    }

    [TestFixture]
    public class EnumCsGeneratorTests
    {
        [Test]
        public void CreatesCorrectCsText()
        {
            string csText = EnumCsGenerator.GenerateEnumCs(Enum.GetNames(typeof(TestEnum)), nameof(TestEnum), "TestNamespace");
            
            const string expectedCsText = @"namespace TestNamespace
{
	public enum TestEnum
	{
		A_B,
		A_B_C,
		A_B_C_D
	}
}";
            //normalize the line endings
            string actualResult = Regex.Replace(csText, @"\r\n?|\n", Environment.NewLine);
            string expectedResult = Regex.Replace(expectedCsText, @"\r\n?|\n", Environment.NewLine);

            Assert.That(csText, Is.EqualTo(expectedCsText));
        }
    }
}
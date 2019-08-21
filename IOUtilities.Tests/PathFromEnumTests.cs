using System;
using System.Collections;
using System.Diagnostics;
using System.IO.Abstractions;
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
        public void ConstructorlessDoesNotThrow()
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
            Debug.WriteLine(csText);

            string expectedCsText = $"namespace TestNamespace{Environment.NewLine}{{{Environment.NewLine}\tpublic enum TestEnum{Environment.NewLine}\t{{{Environment.NewLine}\t\tA_B,{Environment.NewLine}\t\tA_B_C,{Environment.NewLine}\t\tA_B_C_D{Environment.NewLine}\t}}{Environment.NewLine}}}";
            Assert.That(csText, Is.EqualTo(expectedCsText));
        }
    }
}
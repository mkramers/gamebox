using System;
using System.Diagnostics;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using NUnit.Framework;

namespace IOUtilities.Tests
{
    [TestFixture]
    public class PathExtensionsTests
    {
        private static readonly IPath m_path = new MockFileSystem().Path;

        [Test]
        public void TestCanCalculateRelativePath()
        {
            const string rootPath = ".\\windows";
            const string fullPath = ".\\windows\\system32\\wininet.dll";
            const string expectedResult = ".\\system32\\wininet.dll";

            string result = m_path.GetRelativePath(rootPath, fullPath);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestDifferencesDueToBackstepsDoesNotMatter()
        {
            string format1 = m_path.NormalizeFilepath(".\\windows\\system32");
            string format2 = m_path.NormalizeFilepath(".\\Program Files\\..\\Windows\\System32");

            Assert.AreEqual(format1, format2);
        }

        [Test]
        public void TestDifferencesInCapitalizationDoesNotMatter()
        {
            string format1 = m_path.NormalizeFilepath(".\\windows\\system32");
            string format2 = m_path.NormalizeFilepath(".\\WindowS\\System32");

            Assert.AreEqual(format1, format2);
        }

        [Test]
        public void TestDifferencesInFinalSlashDoesNotMatter()
        {
            string format1 = m_path.NormalizeFilepath(".\\windows\\system32");
            string format2 = m_path.NormalizeFilepath(".\\windows\\system32\\");

            Assert.AreEqual(format1, format2);
        }

        [Test]
        public void TestThrowsExceptionIfRootDoesNotMatchFullPath()
        {
            const string rootPath = ".\\windows";
            const string fullPath = ".\\program files\\Internet Explorer\\iexplore.exe";

            Assert.Throws<Exception>(() => m_path.GetRelativePath(rootPath, fullPath));
        }
    }
}
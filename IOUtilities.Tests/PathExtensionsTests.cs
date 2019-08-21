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
            string rootPath = m_path.Combine(".", "windows");
            string fullPath = m_path.Combine(".", "windows", "system32", "wininet.dll");
            string expectedResult = m_path.Combine(".", "system32","wininet.dll");

            string result = m_path.GetRelativePath(rootPath, fullPath);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestDifferencesDueToBackstepsDoesNotMatter()
        {
            string format1 = m_path.NormalizeFilepath(m_path.Combine(".", "Windows", "System32"));
            string format2 = m_path.NormalizeFilepath(m_path.Combine(".", "Program Files", "..", "Windows", "System32"));

            Assert.AreEqual(format1, format2);
        }
        
        [Test]
        public void TestDifferencesInFinalSlashDoesNotMatter()
        {
            string format1 = m_path.NormalizeFilepath(m_path.Combine(".", "windows", "system32"));
            string format2 = m_path.NormalizeFilepath(m_path.Combine(".", "windows", "system32", ".").TrimEnd('.'));

            Assert.AreEqual(format1, format2);
        }

        [Test]
        public void TestThrowsExceptionIfRootDoesNotMatchFullPath()
        {
            string rootPath = m_path.Combine(".", "windows");
            string fullPath = m_path.Combine(".", "program files", "Internet Explorer", "iexplore.exe");

            Assert.Throws<Exception>(() => m_path.GetRelativePath(rootPath, fullPath));
        }
    }
}
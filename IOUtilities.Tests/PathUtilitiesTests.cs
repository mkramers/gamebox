using System;
using NUnit.Framework;

namespace IOUtilities.Tests
{
    [TestFixture]
    public class PathUtilitiesTests
    {
        [Test]
        public void TestCanCalculateRelativePath()
        {
            const string rootPath = ".\\windows";
            const string fullPath = ".\\windows\\system32\\wininet.dll";
            const string expectedResult = ".\\system32\\wininet.dll";

            string result = PathUtilities.GetRelativePath(rootPath, fullPath);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestDifferencesDueToBackstepsDoesNotMatter()
        {
            string format1 = PathUtilities.NormalizeFilepath(".\\windows\\system32");
            string format2 = PathUtilities.NormalizeFilepath(".\\Program Files\\..\\Windows\\System32");

            Assert.AreEqual(format1, format2);
        }

        [Test]
        public void TestDifferencesInCapitalizationDoesNotMatter()
        {
            string format1 = PathUtilities.NormalizeFilepath(".\\windows\\system32");
            string format2 = PathUtilities.NormalizeFilepath(".\\WindowS\\System32");

            Assert.AreEqual(format1, format2);
        }

        [Test]
        public void TestDifferencesInFinalSlashDoesNotMatter()
        {
            string format1 = PathUtilities.NormalizeFilepath(".\\windows\\system32");
            string format2 = PathUtilities.NormalizeFilepath(".\\windows\\system32\\");

            Console.WriteLine(format1);
            Console.WriteLine(format2);

            Assert.AreEqual(format1, format2);
        }

        [Test]
        public void TestThrowsExceptionIfRootDoesNotMatchFullPath()
        {
            const string rootPath = ".\\windows";
            const string fullPath = ".\\program files\\Internet Explorer\\iexplore.exe";

            try
            {
                PathUtilities.GetRelativePath(rootPath, fullPath);
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail("Exception expected");
        }
    }
}
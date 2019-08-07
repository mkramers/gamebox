using System;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class PathUtilitiesTests
    {
        [Test]
        public void TestDifferencesInCapitolizationDontMatter()
        {
            string format1 = PathUtilities.NormalizeFilepath("c:\\windows\\system32");
            string format2 = PathUtilities.NormalizeFilepath("c:\\WindowS\\System32");

            Assert.AreEqual(format1, format2);
        }

        [Test]
        public void TestDifferencesDueToBackstepsDontMatter()
        {
            string format1 = PathUtilities.NormalizeFilepath("c:\\windows\\system32");
            string format2 = PathUtilities.NormalizeFilepath("c:\\Program Files\\..\\Windows\\System32");

            Assert.AreEqual(format1, format2);
        }

        [Test]
        public void TestDifferencesInFinalSlashDontMatter()
        {
            string format1 = PathUtilities.NormalizeFilepath("c:\\windows\\system32");
            string format2 = PathUtilities.NormalizeFilepath("c:\\windows\\system32\\");

            Console.WriteLine(format1);
            Console.WriteLine(format2);

            Assert.AreEqual(format1, format2);
        }

        [Test]
        public void TestCanCalculateRelativePath()
        {
            string rootPath = "c:\\windows";
            string fullPath = "c:\\windows\\system32\\wininet.dll";
            string expectedResult = ".\\system32\\wininet.dll";

            string result = PathUtilities.GetRelativePath(rootPath, fullPath);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestThrowsExceptionIfRootDoesntMatchFullPath()
        {
            string rootPath = "c:\\windows";
            string fullPath = "c:\\program files\\Internet Explorer\\iexplore.exe";

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
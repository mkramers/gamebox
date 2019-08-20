using System.Collections;
using System.Diagnostics;
using Common.Extensions;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class FloatExtensionsTests
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(0.1f, 5.72957802f);
                yield return new TestCaseData(-10.1f, 141.312622f);
                yield return new TestCaseData(2.66f, 152.406769f);
                yield return new TestCaseData(8.1f, 104.095825f);
            }
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void DegreesCorrect(float _radians, float _expectedDegrees)
        {
            float actualDegrees = _radians.ToDegrees();

            Debug.WriteLine(actualDegrees);

            Assert.That(actualDegrees, Is.EqualTo(_expectedDegrees).Within(0.00001f));
        }
    }
}
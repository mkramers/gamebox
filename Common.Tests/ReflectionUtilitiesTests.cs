using System;
using System.Collections.Generic;
using System.Reflection;
using Common.ReflectionUtilities;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class ReflectionUtilitiesTests
    {
        public class TestTypeBase
        { }

        public class TestType : TestTypeBase
        { }

        public class OtherTestType
        { }

        [Test]
        public void FindsAllDerivedTypesCorrectlyGivenAssembly()
        {
            Type[] expectedTypes = {
                typeof(TestType),
            };

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = currentAssembly.FindAllDerivedTypes<TestTypeBase>();

            Assert.That(expectedTypes, Is.EquivalentTo(types));
        }
        [Test]
        public void FindsAllDerivedTypesCorrectly()
        {
            Type[] expectedTypes = {
                typeof(TestType),
            };

            IEnumerable<Type> types =
                ReflectionUtilities.ReflectionUtilities.FindAllDerivedTypes<TestTypeBase>();

            Assert.That(expectedTypes, Is.EquivalentTo(types));
        }
    }
}
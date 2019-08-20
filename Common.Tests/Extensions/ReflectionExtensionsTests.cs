using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Extensions;
using NUnit.Framework;

namespace Common.Tests.Extensions
{
    [TestFixture]
    public class ReflectionExtensionsTests
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
                ReflectionExtensions.FindAllDerivedTypes<TestTypeBase>();

            Assert.That(expectedTypes, Is.EquivalentTo(types));
        }
    }
}
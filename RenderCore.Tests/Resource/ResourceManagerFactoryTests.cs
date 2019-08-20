using NUnit.Framework;
using RenderCore.Resource;

namespace RenderCore.Tests.Resource
{
    [TestFixture]
    public class ResourceManagerFactoryTests
    {
        [Test]
        public void ReturnsNotNull()
        {
            ResourceManagerFactory<TestEnum> factory = new ResourceManagerFactory<TestEnum>();
            ResourceManager<TestEnum> resourceManager = factory.Create("");

            Assert.That(resourceManager, Is.Not.Null);
        }
    }
}
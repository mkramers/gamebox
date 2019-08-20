using IOUtilities;
using Moq;
using NUnit.Framework;
using RenderCore.Resource;

namespace RenderCore.Tests
{
    [TestFixture]
    public class ResourceManagerBaseTests
    {
        public enum TestEnum
        {
            A,
            B,
            C
        }

        [Test]
        public void ReturnsResourceCorrectly()
        {
            const string path = "path";

            Mock<IPathConverter<TestEnum>> mockPathConverter = new Mock<IPathConverter<TestEnum>>();
            mockPathConverter.Setup(_pathConverter => _pathConverter.GetPath(It.IsAny<TestEnum>())).Returns(path);

            ResourceManagerBase<TestEnum, string> resourceManager = new ResourceManagerBase<TestEnum, string>(mockPathConverter.Object, _path => _path);
            Resource<string> resource = resourceManager.GetResource(TestEnum.A);
            string actualResource = resource.Load();

            Assert.That(actualResource, Is.EqualTo(path));
        }

        [Test]
        public void ReturnsCachedResource()
        {
            const string path = "path";

            Mock<IPathConverter<TestEnum>> mockPathConverter = new Mock<IPathConverter<TestEnum>>();
            mockPathConverter.Setup(_pathConverter => _pathConverter.GetPath(It.IsAny<TestEnum>())).Returns(path);

            ResourceManagerBase<TestEnum, string> resourceManager = new ResourceManagerBase<TestEnum, string>(mockPathConverter.Object, _path => _path);
            Resource<string> expectedResource = resourceManager.GetResource(TestEnum.A);
            Resource<string> actualResource = resourceManager.GetResource(TestEnum.A);

            Assert.That(expectedResource, Is.EqualTo(actualResource));
        }
    }
}

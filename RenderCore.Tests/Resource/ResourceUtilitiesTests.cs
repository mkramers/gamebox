using System.Text;
using NUnit.Framework;
using RenderCore.Resource;

namespace RenderCore.Tests.Resource
{
    [TestFixture]
    public class ResourceUtilitiesTests
    {
        [Test]
        public void TestReturnsResourceStream()
        {
            byte[] resourceData = ResourceUtilities.GetResourceData("RenderCore.Tests.Resources.resource.txt");
            string actualResource = Encoding.Default.GetString(resourceData);
            const string expectedResource = "test";
            Assert.That(actualResource, Is.EqualTo(expectedResource));
        }
    }
}
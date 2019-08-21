using System.Drawing;
using Moq;
using NUnit.Framework;
using RenderCore.Resource;
using SFML.Graphics;

namespace RenderCore.Tests.Resource
{
    [TestFixture]
    public class ResourceManagerTests
    {
        //[Test]
        //public void ReturnsValidTexture()
        //{
        //    const string path = "path";

        //    Texture expectedTexture = new Texture(1, 1);
        //    Bitmap expectedBitmap = new Bitmap(1, 1);

        //    Mock<IResourceManager<TestEnum, Texture>> mockTextureManager = new Mock<IResourceManager<TestEnum, Texture>>();
        //    mockTextureManager.Setup(_resourceManager => _resourceManager.GetResource(It.IsAny<TestEnum>()))
        //        .Returns(new Resource<Texture>(_path => expectedTexture, path));

        //    Mock<IResourceManager<TestEnum, Bitmap>> mockBitmapManager = new Mock<IResourceManager<TestEnum, Bitmap>>();
        //    mockBitmapManager.Setup(_resourceManager => _resourceManager.GetResource(It.IsAny<TestEnum>()))
        //        .Returns(new Resource<Bitmap>(_path => expectedBitmap, path));
            
        //    ResourceManager<TestEnum> resourceManager = new ResourceManager<TestEnum>(mockTextureManager.Object, mockBitmapManager.Object);

        //    Resource<Texture> textureResource = resourceManager.GetTextureResource(TestEnum.A);
        //    Texture actualTexture = textureResource.Load();

        //    Resource<Bitmap> bitmapResource = resourceManager.GetBitmapResource(TestEnum.A);
        //    Bitmap actualBitmap = bitmapResource.Load();
            
        //    Assert.Multiple(() =>
        //    {
        //        Assert.That(expectedTexture, Is.EqualTo(actualTexture));
        //        Assert.That(expectedBitmap, Is.EqualTo(actualBitmap));
        //    });
        //}
    }
}

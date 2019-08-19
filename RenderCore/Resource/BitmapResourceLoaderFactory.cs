using System.Drawing;

namespace RenderCore.Resource
{
    public sealed class BitmapResourceLoaderFactory : IResourceLoaderFactory<Bitmap>
    {
        public IResourceLoader<Bitmap> CreateResourceLoader(string _resourcePath)
        {
            return new BitmapFileLoader(_resourcePath);
        }
    }
}
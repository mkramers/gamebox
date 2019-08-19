using System.Drawing;

namespace RenderCore.Resource
{
    public class BitmapResourceLoaderFactory : IResourceLoaderFactory<Bitmap>
    {
        public virtual IResourceLoader<Bitmap> CreateResourceLoader(string _resourcePath)
        {
            return new BitmapFileLoader(_resourcePath);
        }
    }
}
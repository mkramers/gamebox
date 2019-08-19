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

    public class BitmapFileLoader : ResourceLoaderBase<Bitmap>
    {
        public BitmapFileLoader(string _bitmapFilePath) : base(_bitmapFilePath)
        {
        }

        public override Bitmap Load()
        {
            return new Bitmap(m_resourcePath);
        }
    }
}
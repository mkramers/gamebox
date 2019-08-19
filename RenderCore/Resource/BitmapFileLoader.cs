using System.Drawing;

namespace RenderCore.Resource
{
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
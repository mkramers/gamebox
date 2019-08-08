using System.Drawing;

namespace RenderCore.Resource
{
    public class BitmapFileLoader : IResourceLoader<Bitmap>
    {
        private readonly string m_bitmapFilePath;

        public BitmapFileLoader(string _bitmapFilePath)
        {
            m_bitmapFilePath = _bitmapFilePath;
        }

        public Bitmap Load()
        {
            return new Bitmap(m_bitmapFilePath);
        }
    }
}
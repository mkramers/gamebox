using System;
using System.Drawing;

namespace RenderCore.Resource
{
    public class BitmapResourceManager<T> : ResourceManagerBase<T, Bitmap> where T : Enum
    {
        public BitmapResourceManager(IResourceLoaderFactory<Bitmap> _resourceLoaderFactory, IPathConverter<T> _pathConverter) : base(_resourceLoaderFactory, _pathConverter)
        {
        }
    }
}
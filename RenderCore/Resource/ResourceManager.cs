using System;
using System.Drawing;
using SFML.Graphics;

namespace RenderCore.Resource
{
    /// <summary>
    ///     https://gamedev.stackexchange.com/a/2246/31371
    /// </summary>
    public class ResourceManager<T> where T : Enum
    {
        private readonly IResourceManager<T, Bitmap> m_bitmapResourceManager;
        private readonly IResourceManager<T, Texture> m_textureResourceManager;

        public ResourceManager(IResourceManager<T, Texture> _textureResourceManager, IResourceManager<T, Bitmap> _bitmapResourceManager)
        {
            m_textureResourceManager = _textureResourceManager;
            m_bitmapResourceManager = _bitmapResourceManager;
        }

        public Resource<Texture> GetTextureResource(T _id)
        {
            return m_textureResourceManager.GetResource(_id);
        }

        public Resource<Bitmap> GetBitmapResource(T _id)
        {
            return m_bitmapResourceManager.GetResource(_id);
        }
    }
}
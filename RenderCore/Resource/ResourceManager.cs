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
        private readonly BitmapResourceManager<T> m_bitmapResourceManager;
        private readonly TextureResourceManager<T> m_textureResourceManager;

        public ResourceManager(TextureResourceManager<T> _textureResourceManager, BitmapResourceManager<T> _bitmapResourceManager)
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
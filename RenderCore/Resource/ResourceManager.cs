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

        public ResourceManager(string _resourceRootDirectory)
        {
            m_textureResourceManager = new TextureResourceManager<T>(_resourceRootDirectory);
            m_bitmapResourceManager = new BitmapResourceManager<T>(_resourceRootDirectory);
        }

        public Resource<Texture> GetTextureResource(T _id)
        {
            return m_textureResourceManager.GetTextureResource(_id);
        }

        public Resource<Bitmap> GetBitmapResource(T _id)
        {
            return m_bitmapResourceManager.GetBitmapResource(_id);
        }
    }
}
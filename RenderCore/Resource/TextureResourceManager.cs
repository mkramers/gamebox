using System;
using System.IO.Abstractions;
using SFML.Graphics;

namespace RenderCore.Resource
{
    public class TextureResourceManager<T> : ResourceManagerBase<T, Texture> where T : Enum
    {
        public TextureResourceManager(IResourceLoaderFactory<Texture> _resourceLoaderFactory, IPathConverter<T> _pathConverter) : base(_resourceLoaderFactory, _pathConverter)
        {
        }
    }
}
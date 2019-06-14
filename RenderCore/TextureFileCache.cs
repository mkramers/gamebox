using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
    public interface ITextureArgs : IEquatable<ITextureArgs>
    {
        Texture GetTexture();
    }

    public class TextureFileArgs : ITextureArgs
    {
        private string FileName { get; }
        public IntRect? Area { get; }

        public TextureFileArgs(string _fileName, IntRect? _area = null)
        {
            FileName = _fileName;
            Area = _area;
        }

        public Texture GetTexture()
        {
            Texture texture = Area != null
                ? new Texture(FileName, Area.Value)
                : new Texture(FileName);
            return texture;
        }

        public bool Equals(ITextureArgs _other)
        {
            TextureFileArgs otherTextureArgs = _other as TextureFileArgs;
            if (otherTextureArgs == null)
            {
                return false;
            }

            return Area.Equals(otherTextureArgs.Area) && FileName.Equals(otherTextureArgs.FileName);
        }
    }

    public class TextureResourceArgs : ITextureArgs
    {
        public bool Equals(ITextureArgs _other)
        {
            TextureResourceArgs otherTextureArgs = _other as TextureResourceArgs;
            if (otherTextureArgs == null)
            {
                return false;
            }

            return Area.Equals(otherTextureArgs.Area) && ResourceName.Equals(otherTextureArgs.ResourceName);
        }

        public IntRect? Area { get; }

        public string ResourceName { get; }

        public TextureResourceArgs(string _resourceName, IntRect? _area = null)
        {
            ResourceName = _resourceName;
            Area = _area;
        }

        public Texture GetTexture()
        {
            byte[] resourceData = ResourceUtilities.GetResourceData(ResourceName);

            Image image = new Image(resourceData);

            Texture texture = Area != null
                ? new Texture(image, Area.Value)
                : new Texture(image);
            return texture;
        }
    }

    public class TextureProvider : ICacheObjectProvider<Texture, ITextureArgs>
    {
        public Texture GetObject(ITextureArgs _args)
        {
            Texture texture = _args.GetTexture();
            return texture;
        }
    }

    public interface ICacheEntry<out T, out TY> where T : class where TY : IEquatable<TY>
    {
        TY Id { get; }
        T CachedObject { get; }
    }
    
    public interface ICacheObjectProvider<out T, in TY> where T : class where TY : IEquatable<TY>
    {
        T GetObject(TY _id);
    }

    public class CacheEntry<T, TY> : ICacheEntry<T, TY> where T : class where TY : IEquatable<TY>
    {
        public T CachedObject { get; }
        public TY Id { get; }

        public CacheEntry(TY _id, T _cachedObject)
        {
            Id = _id;
            CachedObject = _cachedObject;
        }
    }

    public abstract class Cache<T, TY> where T : class where TY : IEquatable<TY>
    {
        private readonly ICacheObjectProvider<T, TY> m_cacheObjectProvider;
        private readonly List<ICacheEntry<T, TY>> m_entries;

        protected Cache(ICacheObjectProvider<T, TY> _cacheObjectProvider)
        {
            m_cacheObjectProvider = _cacheObjectProvider;
            m_entries = new List<ICacheEntry<T, TY>>();
        }

        protected T GetObject(TY _id)
        {
            T cachedObject;

            ICacheEntry<T, TY> existingEntry = m_entries.Find(_entry => _entry.Id.Equals(_id));
            if (existingEntry == null)
            {
                cachedObject = m_cacheObjectProvider.GetObject(_id);

                ICacheEntry<T, TY> entry = new CacheEntry<T, TY>(_id, cachedObject);
                m_entries.Add(entry);
            }
            else
            {
                cachedObject = existingEntry.CachedObject;
            }

            return cachedObject;
        }
    }

    public abstract class TextureCache : Cache<Texture, ITextureArgs>
    {
        protected TextureCache(ICacheObjectProvider<Texture, ITextureArgs> _cacheObjectProvider) : base(_cacheObjectProvider)
        {
        }

    }

    public class TextureFileCache : TextureCache
    {
        private TextureFileCache(ICacheObjectProvider<Texture, ITextureArgs> _cacheObjectProvider) : base(_cacheObjectProvider)
        {
        }

        public Texture GetTextureFromFile(string _fileName, IntRect? _area = null)
        {
            TextureFileArgs args = new TextureFileArgs(_fileName, _area);
            Texture texture = GetObject(args);
            return texture;
        }

        public Texture GetTextureFromResource(string _resourceName, IntRect? _area = null)
        {
            TextureResourceArgs args = new TextureResourceArgs(_resourceName, _area);
            Texture texture = GetObject(args);
            return texture;
        }

        public static TextureFileCache Instance { get; } = Factory.CreateTextureCache();

        private static class Factory
        {
            internal static TextureFileCache CreateTextureCache()
            {
                TextureProvider textureProvider = new TextureProvider();
                return new TextureFileCache(textureProvider);
            }
        }

    }
}

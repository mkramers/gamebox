using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
    public class TextureId : IEquatable<TextureId>
    {
        public bool Equals(TextureId _other)
        {
            return string.Equals(FileName, _other.FileName) && Area.Equals(_other.Area);
        }

        public override bool Equals(object _obj)
        {
            if (ReferenceEquals(null, _obj))
            {
                return false;
            }

            if (ReferenceEquals(this, _obj))
            {
                return true;
            }

            if (_obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((TextureId) _obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((FileName != null ? FileName.GetHashCode() : 0) * 397) ^ Area.GetHashCode();
            }
        }

        public TextureId(string _fileName, IntRect? _area = null)
        {
            FileName = _fileName;
            Area = _area;
        }

        public string FileName { get; }
        public IntRect? Area { get; }
    }

    public class FileTextureProvider : ICacheObjectProvider<Texture, TextureId>
    {
        public Texture GetObject(TextureId _args)
        {
            Texture texture = _args.Area != null
                ? new Texture(_args.FileName, _args.Area.Value)
                : new Texture(_args.FileName);
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

    public class TextureCache : Cache<Texture, TextureId>
    {
        public TextureCache(ICacheObjectProvider<Texture, TextureId> _cacheObjectProvider) : base(_cacheObjectProvider)
        {
        }

        public Texture GetTexture(string _fileName, IntRect? _area = null)
        {
            TextureId args = new TextureId(_fileName, _area);
            Texture texture = GetObject(args);
            return texture;
        }
        
        public static TextureCache Instance { get; } = Factory.CreateTextureCache();

        private static class Factory
        {
            internal static TextureCache CreateTextureCache()
            {
                FileTextureProvider textureProvider = new FileTextureProvider();
                return new TextureCache(textureProvider);
            }
        }

    }
}

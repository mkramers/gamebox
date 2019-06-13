using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class TextureId : IEquatable<TextureId>
    {
        public TextureId(IntRect? _area = null)
        {
            Area = _area;
        }

        public abstract Texture GetTexture();

        public IntRect? Area { get; }
        public abstract bool Equals(TextureId other);
    }

    public class TextureFileId : TextureId
    {
        public override bool Equals(TextureId _other)
        {
            return Equals(_other as object);
        }

        private bool Equals(TextureFileId _other)
        {
            return Area.Equals(_other.Area) && string.Equals(FileName, _other.FileName);
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

            return Equals((TextureFileId) _obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Area.GetHashCode() * 397) ^ (FileName != null ? FileName.GetHashCode() : 0);
            }
        }

        public string FileName { get; }

        public TextureFileId(string _fileName, IntRect? _area = null) : base(_area)
        {
            FileName = _fileName;
        }

        public override Texture GetTexture()
        {
            Texture texture = Area != null
                ? new Texture(FileName, Area.Value)
                : new Texture(FileName);
            return texture;
        }
    }

    public class TextureResourceId : TextureId
    {
        public override bool Equals(TextureId _other)
        {
            return Equals(_other as object);
        }

        private bool Equals(TextureResourceId _other)
        {
            return Area.Equals(_other.Area) && string.Equals(ResourceName, _other.ResourceName);
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

            return Equals((TextureResourceId)_obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Area.GetHashCode() * 397) ^ (ResourceName != null ? ResourceName.GetHashCode() : 0);
            }
        }

        public string ResourceName { get; }

        public TextureResourceId(string _resourceName, IntRect? _area = null) : base(_area)
        {
            ResourceName = _resourceName;
        }

        public override Texture GetTexture()
        {
            byte[] resourceData = ResourceUtilities.GetResourceData(ResourceName);

            Image image = new Image(resourceData);

            Texture texture = Area != null
                ? new Texture(image, Area.Value)
                : new Texture(image);
            return texture;
        }
    }

    public class FileTextureProvider : ICacheObjectProvider<Texture, TextureId>
    {
        public Texture GetObject(TextureId _args)
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

    public class TextureCache : Cache<Texture, TextureId>
    {
        private TextureCache(ICacheObjectProvider<Texture, TextureId> _cacheObjectProvider) : base(_cacheObjectProvider)
        {
        }

        public Texture GetTextureFromFile(string _fileName, IntRect? _area = null)
        {
            TextureFileId args = new TextureFileId(_fileName, _area);
            Texture texture = GetObject(args);
            return texture;
        }
        public Texture GetTextureFromResource(string _resourceName, IntRect? _area = null)
        {
            TextureResourceId args = new TextureResourceId(_resourceName, _area);
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

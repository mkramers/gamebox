using System;

namespace RenderCore
{
    public interface ICacheEntry<out T, out TY> where T : class where TY : IEquatable<TY>
    {
        TY Id { get; }
        T CachedObject { get; }
    }
}
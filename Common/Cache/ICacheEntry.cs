namespace Common.Cache
{
    public interface ICacheEntry<out T, out TY> where T : class
    {
        TY Id { get; }
        T CachedObject { get; }
    }
}
using Common.Cache;

namespace Common.Caches
{
    public class StringCache<T> : Cache<T, string> where T : class
    {
    }
}
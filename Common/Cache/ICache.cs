namespace Common.Cache
{
    public interface ICache<TValue, in TType> where TValue : class
    {
        TValue GetObject(TType _id);
        bool EntryExists(TType _id);
        void AddObject(TType _id, TValue _value);
    }
}
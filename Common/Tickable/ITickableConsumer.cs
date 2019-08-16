namespace Common.Tickable
{
    public interface ITickableConsumer
    {
        void AddTickableProvider(ITickableProvider _tickableProvider);
        void RemoveTickableProvider(ITickableProvider _tickableProvider);
    }
}
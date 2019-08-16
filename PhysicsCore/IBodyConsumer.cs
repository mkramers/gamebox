namespace PhysicsCore
{
    public interface IBodyConsumer
    {
        void AddBodyProvider(IBodyProvider _bodyProvider);
        void RemoveBodyProvider(IBodyProvider _bodyProvider);
    }
}
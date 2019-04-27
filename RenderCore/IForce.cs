namespace RenderCore
{
    public interface IForce
    {
        void Add(IForce _force);
        void Subtract(IForce _force);
    }
}
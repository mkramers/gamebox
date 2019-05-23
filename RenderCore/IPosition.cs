using System.Numerics;

namespace RenderCore
{
    public interface IPosition
    {
        Vector2 GetPosition();
        void SetPosition(Vector2 _position);
    }
}
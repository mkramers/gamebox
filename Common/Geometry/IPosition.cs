using System.Numerics;

namespace Common.Geometry
{
    public interface IPosition
    {
        Vector2 GetPosition();
        void SetPosition(Vector2 _position);
    }
}
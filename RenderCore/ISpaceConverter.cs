using System.Numerics;

namespace RenderCore
{
    public interface ISpaceConverter
    {
        Vector2 Transform(Vector2 _vector);
        Vector2 TransformScale(Vector2 _vector);
    }
}
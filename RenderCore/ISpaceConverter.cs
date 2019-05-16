using System.Numerics;

namespace RenderCore
{
    public interface ISpaceConverter
    {
        Vector2 ConvertTo(Vector2 _vector);
        Vector2 ConvertFrom(Vector2 _vector);
    }
}
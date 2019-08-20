using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Common.Extensions
{
    public static class Vector2Extensions
    {
        public static string GetDisplayString(this IEnumerable<Vector2> _vectors)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Vector2 vector in _vectors)
            {
                string displayString = vector.GetDisplayString();
                stringBuilder.AppendLine(displayString);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetDisplayString(this Vector2 _vector)
        {
            return $"{{{_vector.X}, {_vector.Y}}}";
        }

        public static bool ApproximatelyEqualTo(this Vector2 _vectorA, Vector2 _vectorB)
        {
            const float tolerance = 0.0001f;
            return (_vectorA - _vectorB).Length() < tolerance;
        }
    }
}
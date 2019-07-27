using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Common.Geometry
{
    public static class Vector2Extensions
    {
        public static IList<double[]> GetDoubleArrays(this IEnumerable<Vector2> _vectors)
        {
            List<double[]> doubleArrays = _vectors.Select(_vector => new double[] {_vector.X, _vector.Y}).ToList();
            return doubleArrays;
        }

        public static IEnumerable<Vector2> FromDoubleArrays(this IEnumerable<double[]> _doubleArrays)
        {
            IEnumerable<Vector2> vectors = _doubleArrays.Select(_doubleArray =>
                new Vector2((float) _doubleArray[0], (float) _doubleArray[1]));
            return vectors;
        }

        public static string GetDisplayString(this IEnumerable<Vector2> _vectors)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Vector2 vector in _vectors)
            {
                string displayString = vector.GetDisplayString();
                stringBuilder.AppendLine(displayString);
            }

            return stringBuilder.ToString();
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
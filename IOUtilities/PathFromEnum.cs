using System;
using System.IO;
using System.Linq;

namespace IOUtilities
{
    public class PathFromEnum<T> where T : Enum
    {
        public string GetPathFromEnum(T _enumValue)
        {
            string[] segments = _enumValue.ToString().ToLower()
                .Split(new[] {"_"}, StringSplitOptions.RemoveEmptyEntries);

            string className = segments.Take(segments.Length - 2).Aggregate("", Path.Combine);
            string spriteName = segments.ElementAt(segments.Length - 2);
            string layerName = segments.Last();

            string path = Path.Combine(className, spriteName, $"{spriteName}-{layerName}");

            return path;
        }
    }
}
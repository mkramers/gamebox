using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IOUtilities
{
    public static class PathFromEnum<T> where T : Enum
    {
        public static Dictionary<T, string> GetPathsFromEnum(string _rootDirectory, string _extension)
        {
            Dictionary<T, string> enumPaths = new Dictionary<T, string>();

            Array enumValues = Enum.GetValues(typeof(T));
            foreach (T enumValue in enumValues)
            {
                string enumPath = GetPathFromEnum(enumValue, _rootDirectory, _extension);
                enumPaths.Add(enumValue, enumPath);
            }

            return enumPaths;
        }

        public static string GetPathFromEnum(T _enumValue, string _rootDirectory, string _extension)
        {
            string[] segments = _enumValue.ToString().ToLower()
                .Split(new[] {"_"}, StringSplitOptions.RemoveEmptyEntries);

            string className = segments.Take(segments.Length - 2).Aggregate(_rootDirectory, Path.Combine);
            string spriteName = segments.ElementAt(segments.Length - 2);
            string layerName = segments.Last();

            string path = Path.Combine(className, spriteName,
                Path.ChangeExtension($"{spriteName}-{layerName}", _extension));

            return path;
        }
    }
}
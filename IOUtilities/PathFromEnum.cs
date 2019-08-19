using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace IOUtilities
{
    public class PathFromEnum<T> where T : Enum
    {
        private readonly IFileSystem m_fileSystem;

        public PathFromEnum(IFileSystem _fileSystem)
        {
            m_fileSystem = _fileSystem;
        }

        public PathFromEnum() : this(new FileSystem())
        {
            
        }

        public string GetPathFromEnum(T _enumValue, string _extension)
        {
            string[] segments = _enumValue.ToString().ToLower()
                .Split(new[] {"_"}, StringSplitOptions.RemoveEmptyEntries);

            string className = segments.Take(segments.Length - 2).Aggregate("", Path.Combine);
            string spriteName = segments.ElementAt(segments.Length - 2);
            string layerName = segments.Last();

            string path = Path.Combine(className, spriteName,
                Path.ChangeExtension($"{spriteName}-{layerName}", _extension));

            return path;
        }
    }
}
using System;
using System.IO;
using System.IO.Abstractions;

namespace IOUtilities
{
    public static class PathExtensions
    {
        public static string NormalizeFilepath(this IPath _path, string _filepath)
        {
            string result = _path.GetFullPath(_filepath).ToLowerInvariant();

            result = result.TrimEnd(Path.DirectorySeparatorChar);

            return result;
        }

        public static string GetRelativePath(this IPath _path, string _rootPath, string _fullPath)
        {
            _rootPath = _path.NormalizeFilepath(_rootPath);
            _fullPath = _path.NormalizeFilepath(_fullPath);

            if (!_fullPath.StartsWith(_rootPath))
            {
                throw new Exception("Could not find rootPath in fullPath when calculating relative path.");
            }

            return "." + _fullPath.Substring(_rootPath.Length);
        }
    }
}
using System;
using System.IO;

namespace IOUtilities
{
    public static class PathUtilities
    {
        public static string NormalizeFilepath(string _filepath)
        {
            string result = Path.GetFullPath(_filepath).ToLowerInvariant();

            result = result.TrimEnd(Path.DirectorySeparatorChar);

            return result;
        }

        public static string GetRelativePath(string _rootPath, string _fullPath)
        {
            _rootPath = NormalizeFilepath(_rootPath);
            _fullPath = NormalizeFilepath(_fullPath);

            if (!_fullPath.StartsWith(_rootPath))
            {
                throw new Exception("Could not find rootPath in fullPath when calculating relative path.");
            }

            return "." + _fullPath.Substring(_rootPath.Length);
        }
    }
}
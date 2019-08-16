using System;
using System.IO;
using System.Linq;

namespace IOUtilities
{
    public static class EnumFromPath
    {
        public static string GetEnumFromPath(string _filePath, string _rootDirectory)
        {
            string filePathNoExtension = Path.ChangeExtension(_filePath, "")?.TrimEnd('.');
            string relativeDirectory = PathUtilities.GetRelativePath(_rootDirectory, filePathNoExtension);

            string[] names = relativeDirectory.TrimStart('.')
                .Split(new[] {"\\", "-"}, StringSplitOptions.RemoveEmptyEntries);

            string enumName = string.Join("_", names.Select(_name => _name.ToUpper()));

            return enumName;
        }
    }
}
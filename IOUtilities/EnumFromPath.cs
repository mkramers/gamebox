﻿using System;
using System.IO;
using System.Linq;
using System.Text;

namespace IOUtilities
{
    public static class EnumFromPath
    {
        public static string GetEnumFromPath(string _filePath, string _rootDirectory)
        {
            string directoryName = Path.GetDirectoryName(_filePath);
            string relativeDirectory = PathUtilities.GetRelativePath(_rootDirectory, directoryName);

            string[] names = relativeDirectory.TrimStart('.').Split(new[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);

            string enumName = string.Join("_", names.Select(_name => _name.ToUpper()));

            return enumName;
        }
    }

    public static class PathFromEnum<T> where T : Enum
    {
        public static string GetPathFromEnum(T _enumValue, string _rootDirectory)
        {
            string[] segments = _enumValue.ToString().Split(new[] {"_"}, StringSplitOptions.RemoveEmptyEntries);

            return segments.Aggregate(_rootDirectory, Path.Combine);
        }
    }
}
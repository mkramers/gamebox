using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IOUtilities
{
    public static class EnumFromPath
    {
        public static T GetEnumFromPath<T>(string _filePath) where T : Enum
        {
            string enumName = GetEnumFromPath(_filePath);

            T enumValue = (T)Enum.Parse(typeof(T), enumName);
            return enumValue;
        }

        public static string GetEnumFromPath(string _filePath)
        {
            string filePathNoExtension = Path.ChangeExtension(_filePath, "")?.TrimEnd('.');

            List<string> names = filePathNoExtension.TrimStart('.')
                .Split(new[] {"\\", "-"}, StringSplitOptions.RemoveEmptyEntries).ToList();

            names.RemoveAt(names.Count - 2);

            string enumName = string.Join("_", names.Select(_name => _name.ToUpper()));
            return enumName;
        }
    }
}
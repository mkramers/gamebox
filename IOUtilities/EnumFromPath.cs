using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace IOUtilities
{
    public class EnumFromPath
    {
        private readonly IFileSystem m_fileSystem;

        public EnumFromPath(IFileSystem _fileSystem)
        {
            m_fileSystem = _fileSystem;
        }

        public EnumFromPath() : this(new FileSystem())
        {
            
        }

        public T GetEnumFromPath<T>(string _filePath) where T : Enum
        {
            string enumName = GetEnumFromPath(_filePath);

            T enumValue = (T)Enum.Parse(typeof(T), enumName);
            return enumValue;
        }

        public string GetEnumFromPath(string _filePath)
        {
            string filePathNoExtension = m_fileSystem.Path.ChangeExtension(_filePath, "")?.TrimEnd('.');

            List<string> names = filePathNoExtension.TrimStart('.')
                .Split(new[] {"\\", "-"}, StringSplitOptions.RemoveEmptyEntries).ToList();

            names.RemoveAt(names.Count - 2);

            string enumName = string.Join("_", names.Select(_name => _name.ToUpper()));
            return enumName;
        }
    }
}
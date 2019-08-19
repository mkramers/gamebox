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
            string parentDirectory = m_fileSystem.Path.GetDirectoryName(_filePath);
            string filePathNoExtension = m_fileSystem.Path.GetFileNameWithoutExtension(_filePath);

            List<string> directoryNames = parentDirectory.Split(new[] { m_fileSystem.Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> fileNames = filePathNoExtension.Split(new[] {"-"}, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();

            IEnumerable<string> allNames = directoryNames.Concat(fileNames);

            string enumName = string.Join("_", allNames).ToUpper();
            return enumName;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace IOUtilities
{
    public class EnumFromPath
    {
        private readonly IPath m_fileSystemPath;

        public EnumFromPath(IPath _fileSystemPath)
        {
            m_fileSystemPath = _fileSystemPath;
        }

        public EnumFromPath() : this(new PathWrapper(new FileSystem()))
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
            string parentDirectory = m_fileSystemPath.GetDirectoryName(_filePath);
            string filePathNoExtension = m_fileSystemPath.GetFileNameWithoutExtension(_filePath);

            List<string> directoryNames = parentDirectory.Split(new[] { m_fileSystemPath.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> fileNames = filePathNoExtension.Split(new[] {"-"}, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();

            IEnumerable<string> allNames = directoryNames.Concat(fileNames);

            string enumName = string.Join("_", allNames).ToUpper();
            return enumName;
        }
    }
}
using System;
using System.IO.Abstractions;
using IOUtilities;

namespace RenderCore.Resource
{
    public class PathFromEnumPathConverter<T> : IPathConverter<T> where T : Enum
    {
        private readonly string m_rootDirectory;
        private readonly string m_fileExtension;
        private readonly IFileSystem m_fileSystem;

        public PathFromEnumPathConverter(string _rootDirectory, string _fileExtension, IFileSystem _fileSystem)
        {
            m_rootDirectory = _rootDirectory;
            m_fileSystem = _fileSystem;
            m_fileExtension = _fileExtension;
        }
        public string GetPath(T _id)
        {
            PathFromEnum<T> pathFromEnum = new PathFromEnum<T>();

            string fileName = pathFromEnum.GetPathFromEnum(_id);
            string fullFileName = m_fileSystem.Path.ChangeExtension(fileName, m_fileExtension);
            return m_fileSystem.Path.Combine(m_rootDirectory, fullFileName);
        }
    }
}
using System;
using System.IO.Abstractions;

namespace IOUtilities
{
    public class PathFromEnumPathConverter<T> : IPathConverter<T> where T : Enum
    {
        private readonly string m_rootDirectory;
        private readonly string m_fileExtension;
        private readonly IPath m_fileSystemPath;

        public PathFromEnumPathConverter(string _rootDirectory, string _fileExtension, IPath _fileSystemPath)
        {
            m_rootDirectory = _rootDirectory;
            m_fileSystemPath = _fileSystemPath;
            m_fileExtension = _fileExtension;
        }
        public string GetPath(T _id)
        {
            PathFromEnum<T> pathFromEnum = new PathFromEnum<T>();

            string fileName = pathFromEnum.GetPathFromEnum(_id);
            string fullFileName = m_fileSystemPath.ChangeExtension(fileName, m_fileExtension);
            return m_fileSystemPath.Combine(m_rootDirectory, fullFileName);
        }
    }
}
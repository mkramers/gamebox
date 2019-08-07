using System.IO.Abstractions;

namespace IOUtilities
{
    public class FileWriter
    {
        private readonly FileSystem m_fileSystem;

        protected FileWriter(FileSystem _fileSystem)
        {
            m_fileSystem = _fileSystem;
        }

        public FileWriter() : this(new FileSystem())
        {
        }

        public void WriteFile(string _filePath, string _contents)
        {
            m_fileSystem.File.WriteAllText(_filePath, _contents);
        }
    }
}
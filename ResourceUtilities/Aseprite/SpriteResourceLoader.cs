using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using GameResources;
using IOUtilities;

namespace ResourceUtilities.Aseprite
{
    public class SpriteResourceLoader
    {
        private readonly FileSystem m_fileSystem;

        protected SpriteResourceLoader(FileSystem _fileSystem)
        {
            m_fileSystem = _fileSystem;
        }

        public SpriteResourceLoader() : this(new FileSystem())
        {
        }

        public Dictionary<SpriteResources, string> LoadResources(string _rootDirectory)
        {
            const string extension = ".png";

            Dictionary<SpriteResources, string> paths = PathFromEnum<SpriteResources>.GetPathsFromEnum(_rootDirectory, extension);
            return paths;
        }
    }
}

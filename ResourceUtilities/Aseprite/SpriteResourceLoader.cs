using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;
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

        public void LoadResources(string _rootDirectory)
        {
            var paths = PathFromEnum<SpriteResources>.GetPathsFromEnum(".\\");
        }
    }
}

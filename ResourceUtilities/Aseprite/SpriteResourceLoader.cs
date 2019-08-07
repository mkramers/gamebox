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

        public void LoadResources(string _rootDirectory)
        {
            IEnumerable<string> paths = PathFromEnum<SpriteResources>.GetPathsFromEnum(_rootDirectory);

            paths = paths.Select(GetAsepriteFileName);

            foreach (string path in paths)
            {
                SpriteSheetFile spriteSheet = SpriteSheetFileLoader.LoadFromFile(path);

                MapFileLoader loader = new MapFileLoader();
                SpriteLayers spriteLayers = loader.LoadSpriteLayersFromFile(spriteSheet);
            }
        }

        private string GetAsepriteFileName(string _path)
        {
            const string genDirectoryName = "gen";
            const string asespriteExtension = ".json";

            string spriteName = new DirectoryInfo(_path).Name;

            string filePath = Path.Combine(_path, genDirectoryName, Path.ChangeExtension(spriteName, asespriteExtension));

            return filePath;
        }
    }
}

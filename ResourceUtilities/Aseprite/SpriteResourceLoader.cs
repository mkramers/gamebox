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

            List<SpriteLayers> spriteLayers = new List<SpriteLayers>();

            foreach (string path in paths)
            {
                SpriteSheetFile spriteSheet = SpriteSheetFileLoader.LoadFromFile(path);

                MapFileLoader loader = new MapFileLoader();
                SpriteLayers layers = loader.LoadSpriteLayersFromFile(spriteSheet);

                spriteLayers.Add(layers);
            }
        }

        private string GetAsepriteFileName(string _path)
        {
            const string genDirectoryName = "gen";
            const string asepriteExtension = ".json";

            string spriteName = m_fileSystem.DirectoryInfo.FromDirectoryName(_path).Name;

            string filePath = m_fileSystem.Path.Combine(_path, genDirectoryName, m_fileSystem.Path.ChangeExtension(spriteName, asepriteExtension));

            return filePath;
        }
    }
}

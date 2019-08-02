using Common.Geometry;

namespace GameResources
{
    public class MapLayer
    {
        public MapLayer(string _name, IntSize _size, string _filePath)
        {
            Name = _name;
            Size = _size;
            FilePath = _filePath;
        }

        private IntSize Size { get; }
        public string FilePath { get; }
        public string Name { get; }
    }
}
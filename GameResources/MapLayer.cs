using Common.Geometry;

namespace GameResources
{
    public class MapLayer
    {
        public MapLayer(IntSize _size, string _fileName)
        {
            Size = _size;
            FileName = _fileName;
        }

        private IntSize Size { get; }
        public string FileName { get; }
    }
}
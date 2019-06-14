using Common.Math;

namespace GameResources
{
    public class MapLayer
    {
        public MapLayer(IntSize _size, string _fileName)
        {
            Size = _size;
            FileName = _fileName;
        }

        public IntSize Size { get; }
        public string FileName { get; }
    }
}
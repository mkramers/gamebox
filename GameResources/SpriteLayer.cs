namespace GameResources
{
    public class SpriteLayer
    {
        public SpriteLayer(string _name, string _filePath)
        {
            Name = _name;
            FilePath = _filePath;
        }

        public string FilePath { get; }
        public string Name { get; }
    }
}
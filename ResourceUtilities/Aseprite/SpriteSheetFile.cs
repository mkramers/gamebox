namespace ResourceUtilities.Aseprite
{
    public class SpriteSheetFile
    {
        public SpriteSheetFile(SpriteSheet _spriteSheet, string _filePath)
        {
            SpriteSheet = _spriteSheet;
            FilePath = _filePath;
        }

        public SpriteSheet SpriteSheet { get; }
        public string FilePath { get; }
    }
}
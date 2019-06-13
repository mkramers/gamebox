namespace ResourceUtilities.Aseprite
{
    public class SpriteSheetFileLoader
    {
        public SpriteSheetFile LoadFromFile(string _path)
        {
            SpriteSheetLoader spriteSheetLoader = new SpriteSheetLoader();
            SpriteSheet spriteSheet = spriteSheetLoader.LoadFromFile(_path);
            return new SpriteSheetFile(spriteSheet, _path);
        }
    }
}
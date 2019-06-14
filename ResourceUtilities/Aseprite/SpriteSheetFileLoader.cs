namespace ResourceUtilities.Aseprite
{
    public class SpriteSheetFileLoader
    {
        public static SpriteSheetFile LoadFromFile(string _path)
        {
            SpriteSheetLoader spriteSheetLoader = new SpriteSheetLoader();
            SpriteSheet spriteSheet = SpriteSheetLoader.LoadFromFile(_path);
            return new SpriteSheetFile(spriteSheet, _path);
        }
    }
}
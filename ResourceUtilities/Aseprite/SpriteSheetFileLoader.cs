namespace ResourceUtilities.Aseprite
{
    public static class SpriteSheetFileLoader
    {
        public static SpriteSheetFile LoadFromFile(string _path)
        {
            SpriteSheet spriteSheet = SpriteSheetLoader.LoadFromFile(_path);
            return new SpriteSheetFile(spriteSheet, _path);
        }
    }
}
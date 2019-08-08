using SFML.Graphics;

namespace RenderCore.Font
{
    public static class FontSettingsExtensions
    {
        public static FontSettings GetFontSettings(FontId _fontId, float _scale, uint _size, Color _fillColor)
        {
            FontFactory fontFactory = new FontFactory();
            SFML.Graphics.Font font = fontFactory.GetFont(_fontId);
            return new FontSettings(font, _scale, _size, _fillColor);
        }
    }
}
using SFML.Graphics;

namespace RenderCore.Font
{
    public class FontSettings
    {
        public FontSettings(SFML.Graphics.Font _font, float _scale, uint _size, Color _fillColor)
        {
            Scale = _scale;
            Size = _size;
            Font = _font;
            FillColor = _fillColor;
        }

        public float Scale { get; }
        public uint Size { get; }
        public SFML.Graphics.Font Font { get; }
        public Color FillColor { get; }
    }
}
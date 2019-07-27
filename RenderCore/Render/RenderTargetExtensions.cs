using RenderCore.Font;
using SFML.Graphics;
using SFML.System;

namespace RenderCore.Render
{
    public static class TextFactory
    {
        public static Text GenerateText(FontSettings _fontSettings)
        {
            Text textRenderObject = new Text("", _fontSettings.Font, _fontSettings.Size)
            {
                Scale =
                    new Vector2f(_fontSettings.Scale / _fontSettings.Size, _fontSettings.Scale / _fontSettings.Size),
                FillColor = _fontSettings.FillColor
            };

            return textRenderObject;
        }
    }

    public static class RenderTargetExtensions
    {
        public static void Draw(this RenderTarget _target, Texture _texture, RenderStates _states)
        {
            Sprite sceneSprite = new Sprite(_texture);
            _target.Draw(sceneSprite, _states);
        }
    }
}
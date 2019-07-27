using SFML.Graphics;
using SFML.System;

namespace RenderCore.Widget
{
    public static class TextExtensions
    {
        public static void SetTextCenter(this Text _text, Vector2f _center)
        {
            FloatRect globalBounds = _text.GetGlobalBounds();

            Vector2f textPosition = new Vector2f(globalBounds.Left, globalBounds.Top);
            Vector2f offset = new Vector2f(globalBounds.Width / 2.0f, globalBounds.Height / 2.0f);
            Vector2f position = _center - textPosition - offset;
            _text.Position = position;
        }
    }
}
using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class TextWidget : ScreenRenderCoreWidget<Text>
    {
        protected TextWidget(Font _font, Vector2 _renderScale, ISpaceConverter _spaceConverter, Text _textRenderObject) : base(_textRenderObject, _renderScale, _spaceConverter)
        {
        }

        protected void SetMessage(string _message)
        {
            Text textObject = GetRenderObject();
            textObject.DisplayedString = _message;
        }
    }
}
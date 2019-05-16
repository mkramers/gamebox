using System;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class TextWidget : PositionalRenderCoreWidgetBase
    {
        private readonly Text m_text;

        protected TextWidget(Font _font, uint _fontSize, float _fontScale)
        {
            m_text = new Text("", _font, _fontSize)
            { Scale = new Vector2f(_fontScale / _fontSize, _fontScale / _fontSize) };
        }

        protected void SetMessage(string _message)
        {
            m_text.DisplayedString = _message;
        }

        public override void Draw(RenderTarget _target, RenderStates _states)
        {
            _target.Draw(m_text, _states);
        }

        public override void Dispose()
        {
            m_text.Dispose();
        }

        public override void Tick(TimeSpan _elapsed)
        {
            base.Tick(_elapsed);

            m_text.Position = m_screenPosition;
        }
    }
}
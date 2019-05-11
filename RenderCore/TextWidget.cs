using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class TextWidget : RenderCoreViewWidgetBase
    {
        private readonly Text m_text;
        private Vector2 m_positionScreen;

        public TextWidget(Font _font, uint _fontSize, float _fontScale)
        {
            m_text = new Text("", _font, _fontSize)
                {Scale = new Vector2f(_fontScale / _fontSize, _fontScale / _fontSize)};
        }

        public void SetMessage(string _message)
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

            if (m_view == null)
            {
                return;
            }

            Vector2f size = m_view.Size;
            Vector2f position = m_view.Center - size / 2;

            Vector2f positionViewSpace =
                new Vector2f(size.X * m_positionScreen.X, size.Y * m_positionScreen.Y) + position;

            m_text.Position = positionViewSpace;
        }

        public override void SetRenderPosition(Vector2 _positionScreen)
        {
            m_positionScreen = _positionScreen;
        }
    }
}
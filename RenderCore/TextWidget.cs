using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class TextWidget : IRenderCoreWindowWidget
    {
        private readonly Text m_text;

        public TextWidget(Font _font, uint _fontSize)
        {
            m_text = new Text("", _font, _fontSize) {Scale = new Vector2f(2.0f / _fontSize, 2.0f / _fontSize)};
        }

        public void SetMessage(string _message)
        {
            m_text.DisplayedString = _message;
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            _target.Draw(m_text, _states);
        }

        public void Dispose()
        {
            m_text.Dispose();
        }

        public void SetRenderPosition(Vector2 _position)
        {
            m_text.Position = _position.GetVector2F();
        }

        public void SetView(View _view)
        {
        }
    }
}
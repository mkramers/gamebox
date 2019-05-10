using System;
using System.Collections.Generic;
using System.Numerics;
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
    public class GridWidget : IRenderCoreWindowWidget
    {
        private readonly List<Shape> m_shapes;

        public GridWidget()
        {
            m_shapes = new List<Shape>();
        }

        public void Draw(RenderTarget _target, RenderStates _state)
        {
            foreach (Shape shape in m_shapes)
            {
                _target.Draw(shape, _state);
            }
        }

        public void Dispose()
        {
            ClearShapes();
        }

        public void SetRenderPosition(Vector2 _position)
        {
            throw new NotImplementedException();
        }

        public void SetView(View _view)
        {
            ClearShapes();

            Vector2 snappedCenter = new Vector2((float)Math.Round(_view.Center.X), (float)Math.Round(_view.Center.Y));
            View snappedView = new View(snappedCenter.GetVector2F(), _view.Size);

            IEnumerable<Shape> shapes = GridDrawingUtilities.GetGridDrawableFromView(snappedView);
            m_shapes.AddRange(shapes);
        }

        private void ClearShapes()
        {
            foreach (Shape shape in m_shapes)
            {
                shape.Dispose();
            }

            m_shapes.Clear();
        }
    }
}
using SFML.Graphics;

namespace RenderCore
{
    public abstract class RenderWidget<T> : RenderCoreWidgetBase where T : Transformable, Drawable
    {
        private readonly T m_renderObject;

        protected RenderWidget(T _renderObject)
        {
            m_renderObject = _renderObject;
        }

        public override void Draw(RenderTarget _target, RenderStates _states)
        {
            m_renderObject.Draw(_target, _states);
        }

        public override void Dispose()
        {
            m_renderObject.Dispose();
        }

        public T GetRenderObject()
        {
            return m_renderObject;
        }
    }
}
using Common.Tickable;
using LibExtensions.TGUI;
using SFML.Graphics;
using Vector2 = System.Numerics.Vector2;

namespace RenderCore.Widget
{
    public interface IGuiWidget
    {
        void OnViewChanged(View _view);
        TGUI.Widget GetWidget();
    }

    public class GuiWidget : IGuiWidget
    {
        private readonly TGUI.Widget m_widget;
        private readonly Vector2 m_relativePosition;

        public GuiWidget(TGUI.Widget _widget, Vector2 _relativePosition)
        {
            m_widget = _widget;
            m_relativePosition = _relativePosition;
        }

        public void OnViewChanged(View _view)
        {
            Vector2 position = _view.GetAbsolutePosition(m_relativePosition);

            m_widget.SetPosition(position);
        }

        public TGUI.Widget GetWidget()
        {
            return m_widget;
        }
    }

    public interface IWidget : ITickable
    {
    }
}
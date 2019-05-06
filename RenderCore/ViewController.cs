using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class ViewController : IViewController
    {
        private readonly Vector2 m_size;
        private Vector2 m_trackedCenter;

        public ViewController(Vector2 _size)
        {
            m_size = _size;
        }

        public void SetCenter(Vector2 _center)
        {
            m_trackedCenter = _center;
        }

        public View GetView()
        {
            Vector2 calculatedCenter = m_trackedCenter;

            View view = new View(calculatedCenter.GetVector2F(), m_size.GetVector2F());
            return view;
        }
    }
}
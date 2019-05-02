using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class MoveOffsetCommand : MoveCommandBase
    {
        private readonly Vector2 m_offset;

        public MoveOffsetCommand(Transformable _transformable, Vector2 _offset) : base(_transformable)
        {
            m_offset = _offset;
        }

        public override bool CanExecute(object _parameter)
        {
            return true;
        }

        public override void Execute(object _parameter)
        {
            m_transformable.Position += m_offset.GetVector2F();
        }
    }
}
using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class ApplyNormalForceCommand : IMoveCommand
    {
        private readonly NormalForce m_force;
        private readonly BodySprite m_bodySprite;

        public ApplyNormalForceCommand(BodySprite _bodySprite, NormalForce _force)
        {
            m_bodySprite = _bodySprite;
            m_force = _force;
        }

        public bool CanExecute(object _parameter)
        {
            return true;
        }

        public void Execute(object _parameter)
        {
            m_bodySprite.ApplyForce(m_force);
        }

        public event EventHandler CanExecuteChanged;
    }

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
            m_transformable.Position += m_offset.GetVector2f();
        }
    }
}
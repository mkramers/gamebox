using System;

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
}
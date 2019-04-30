using System;

namespace RenderCore
{
    public class ApplyNormalForceCommand : IMoveCommand
    {
        private readonly IPhysicalBody m_physicalBody;
        private readonly NormalForce m_force;

        public ApplyNormalForceCommand(IPhysicalBody _physicalBody, NormalForce _force)
        {
            m_physicalBody = _physicalBody;
            m_force = _force;
        }

        public bool CanExecute(object _parameter)
        {
            return true;
        }

        public void Execute(object _parameter)
        {
            m_physicalBody.ApplyForce(m_force);
        }

        public event EventHandler CanExecuteChanged;
    }
}
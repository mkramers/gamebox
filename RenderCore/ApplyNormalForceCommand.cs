using System;

namespace RenderCore
{
    public class ApplyNormalForceCommand : IMoveCommand
    {
        private readonly IDynamicBody m_dynamicBody;
        private readonly NormalForce m_force;

        public ApplyNormalForceCommand(IDynamicBody _dynamicBody, NormalForce _force)
        {
            m_dynamicBody = _dynamicBody;
            m_force = _force;
        }

        public bool CanExecute(object _parameter)
        {
            return true;
        }

        public void Execute(object _parameter)
        {
            m_dynamicBody.ApplyForce(m_force);
        }

        public event EventHandler CanExecuteChanged;
    }
}
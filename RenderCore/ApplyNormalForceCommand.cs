using System;
using System.Numerics;

namespace RenderCore
{
    public class ApplyNormalForceCommand : IMoveCommand
    {
        private readonly IBody m_body;
        private readonly Vector2 m_force;

        public ApplyNormalForceCommand(IBody _body, Vector2 _force)
        {
            m_body = _body;
            m_force = _force;
        }

        public bool CanExecute(object _parameter)
        {
            return true;
        }

        public void Execute(object _parameter)
        {
            m_body.ApplyForce(m_force);
        }

        public event EventHandler CanExecuteChanged;
    }
}
using System.Numerics;

namespace RenderCore
{
    public class JumpCommand : BodyCommandBase
    {
        private readonly Vector2 m_forceVector;

        public JumpCommand(IBody _body, Vector2 _forceVector) : base(_body)
        {
            m_forceVector = _forceVector;
        }

        public override bool CanExecute(KeyState _keyState)
        {
            return _keyState.IsPressed && !_keyState.PreviousIsPressed;
        }

        public override void Execute(KeyState _keyState)
        {
            m_body.ApplyLinearImpulse(m_forceVector);
        }
    }
}
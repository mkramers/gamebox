using System.Numerics;
using GameCore.Input.Key;
using RenderCore.Physics;

namespace GameCore.Input
{
    public class MoveCommand : BodyCommandBase
    {
        private readonly Vector2 m_forceVector;

        public MoveCommand(IBody _body, Vector2 _forceVector) : base(_body)
        {
            m_forceVector = _forceVector;
        }

        public override void Execute(KeyState _keyState)
        {
            m_body.ApplyForce(m_forceVector);
        }

        public override bool CanExecute(KeyState _keyState)
        {
            return _keyState.IsPressed;
        }
    }
}
using System;
using System.Diagnostics;
using GameCore.Input.Key;
using PhysicsCore;

namespace GameCore.Input
{
    public abstract class BodyCommandBase : IKeyCommand
    {
        protected readonly IBody m_body;

        protected BodyCommandBase(IBody _body)
        {
            m_body = _body;
        }

        public bool CanExecute(object _parameter)
        {
            KeyState keyState = _parameter as KeyState;
            Debug.Assert(keyState != null);

            return CanExecute(keyState);
        }

        public abstract void Execute(KeyState _keyState);

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public abstract bool CanExecute(KeyState _keyState);

        public void Execute(object _parameter)
        {
            KeyState keyState = _parameter as KeyState;
            Debug.Assert(keyState != null);

            Execute(keyState);
        }
    }
}
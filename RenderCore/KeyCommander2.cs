using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Windows.Input;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore
{
    public static class KeyHandlerFactory
    {
        public static KeyHandler CreateKeyHandler(Dictionary<Keyboard.Key, IKeyCommand> _keyCommands)
        {
            KeyStateContainer keyStateContainer = new KeyStateContainer();
            return new KeyHandler(_keyCommands, keyStateContainer);
        }
    }

    public interface IKeyHandler : ITickable
    {

    }

    public class KeyHandler : IKeyHandler
    {
        private readonly Dictionary<Keyboard.Key, IKeyCommand> m_keyCommands;
        private readonly IKeyStateContainer m_keyStateContainer;

        public KeyHandler(Dictionary<Keyboard.Key, IKeyCommand> _keyCommands, IKeyStateContainer _keyStateContainer)
        {
            m_keyCommands = _keyCommands;
            m_keyStateContainer = _keyStateContainer;
        }

        public void Tick(TimeSpan _elapsed)
        {
            foreach ((Keyboard.Key key, IKeyCommand keyCommand) in m_keyCommands)
            {
                m_keyStateContainer.Update(key);

                KeyState keyState = m_keyStateContainer.GetKeyState(key);

                if (keyCommand.CanExecute(keyState))
                {
                    keyCommand.Execute(keyState);
                }
            }
        }
    }

    public interface IKeyStateContainer
    {
        void Update(Keyboard.Key _key);
        KeyState GetKeyState(Keyboard.Key _key);
    }

    public class KeyStateContainer : IKeyStateContainer
    {
        private readonly Dictionary<Keyboard.Key, KeyState> m_keyStates;

        public KeyStateContainer()
        {
            m_keyStates = new Dictionary<Keyboard.Key, KeyState>();
        }

        public void Update(Keyboard.Key _key)
        {
            KeyState keyState = GetKeyState(_key);

            bool isKeyPressed = Keyboard.IsKeyPressed(_key);
            keyState.Update(isKeyPressed);
        }

        public KeyState GetKeyState(Keyboard.Key _key)
        {
            if (m_keyStates.ContainsKey(_key))
            {
                return m_keyStates[_key];
            }

            KeyState keyState = new KeyState();
            m_keyStates.Add(_key, keyState);

            return m_keyStates[_key];
        }
    }

    public interface IKeyCommand : ICommand
    {
        bool CanExecute(KeyState _keyState);
    }

    public class MoveCommand : IKeyCommand
    {
        private readonly IBody m_body;
        private readonly Vector2 m_forceVector;

        public MoveCommand(IBody _body, Vector2 _forceVector)
        {
            m_body = _body;
            m_forceVector = _forceVector;
        }

        public bool CanExecute(object _parameter)
        {
            KeyState keyState = _parameter as KeyState;
            Debug.Assert(keyState != null);

            return CanExecute(keyState);
        }

        public void Execute(object _parameter)
        {
            m_body.ApplyForce(m_forceVector);
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(KeyState _keyState)
        {
            return _keyState.IsPressed;
        }
    }
}

public class KeyState
{
    public bool IsPressed { get; private set; }
    public bool PreviousIsPressed { get; private set; }

    public KeyState()
    {
        IsPressed = false;
        PreviousIsPressed = false;
    }

    public void Update(bool _isPressed)
    {
        IsPressed = _isPressed;
        PreviousIsPressed = IsPressed;
    }
}

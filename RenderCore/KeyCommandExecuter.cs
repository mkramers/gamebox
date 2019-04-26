using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore
{
    public class KeyCommandExecuter : IKeyHandler
    {
        private readonly Dictionary<Keyboard.Key, ICommand> m_keyBindings;

        public KeyCommandExecuter(Dictionary<Keyboard.Key, ICommand> _keyBindings)
        {
            m_keyBindings = _keyBindings;
        }

        public void KeyPressed(KeyEventArgs _e)
        {
            if (!m_keyBindings.ContainsKey(_e.Code))
            {
                return;
            }

            ICommand command = m_keyBindings[_e.Code];
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
    }

    public static class KeyCommandsFactory
    {
        public static Dictionary<Keyboard.Key, ICommand> GetTransformableMoveCommands(Transformable _transformable, float _amount)
        {
            Dictionary<Keyboard.Key, ICommand> commandBindings = new Dictionary<Keyboard.Key, ICommand>();

            MoveOffsetCommand moveUpCommand = new MoveOffsetCommand(_transformable, -_amount * Vector2.UnitY);
            commandBindings.Add(Keyboard.Key.Up, moveUpCommand);

            MoveOffsetCommand moveDownCommand = new MoveOffsetCommand(_transformable, _amount * Vector2.UnitY);
            commandBindings.Add(Keyboard.Key.Down, moveDownCommand);

            MoveOffsetCommand moveLeftCommand = new MoveOffsetCommand(_transformable, -_amount * Vector2.UnitX);
            commandBindings.Add(Keyboard.Key.Left, moveLeftCommand);

            MoveOffsetCommand moveRightCommand = new MoveOffsetCommand(_transformable, _amount * Vector2.UnitX);
            commandBindings.Add(Keyboard.Key.Right, moveRightCommand);

            return commandBindings;
        }
    }

    public interface IMoveCommand : ICommand
    {

    }

    public abstract class MoveCommandBase : IMoveCommand
    {
        protected readonly Transformable m_transformable;

        protected MoveCommandBase(Transformable _transformable)
        {
            m_transformable = _transformable;
        }

        public abstract bool CanExecute(object _parameter);

        public abstract void Execute(object _parameter);

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
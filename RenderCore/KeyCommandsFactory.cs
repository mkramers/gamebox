using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore
{
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
}
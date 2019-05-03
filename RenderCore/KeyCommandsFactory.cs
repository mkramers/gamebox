using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using SFML.Window;

namespace RenderCore
{
    public static class KeyCommandsFactory
    {
        public static Dictionary<Keyboard.Key, IKeyCommand> GetMovementCommands(IBody _body, float _force)
        {
            Dictionary<Keyboard.Key, IKeyCommand> commands = new Dictionary<Keyboard.Key, IKeyCommand>();

            MoveCommand moveUpCommand =
                new MoveCommand(_body, -_force * Vector2.UnitY);
            commands.Add(Keyboard.Key.Up, moveUpCommand);

            MoveCommand moveDownCommand =
                new MoveCommand(_body, _force * Vector2.UnitY);
            commands.Add(Keyboard.Key.Down, moveDownCommand);

            MoveCommand moveLeftCommand =
                new MoveCommand(_body, -_force * Vector2.UnitX);
            commands.Add(Keyboard.Key.Left, moveLeftCommand);

            MoveCommand moveRightCommand =
                new MoveCommand(_body, _force * Vector2.UnitX);
            commands.Add(Keyboard.Key.Right, moveRightCommand);

            JumpCommand jumpCommand =
                new JumpCommand(_body, -_force / 5 * Vector2.UnitY);
            commands.Add(Keyboard.Key.Space, jumpCommand);

            return commands;
        }
    }
}
﻿using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore
{
    public static class KeyCommandsFactory
    {
        public static Dictionary<Keyboard.Key, ICommand> GetBodySpriteCommands(BodySprite _bodySprite, float _amount)
        {
            Dictionary<Keyboard.Key, ICommand> commandBindings = new Dictionary<Keyboard.Key, ICommand>();

            ApplyNormalForceCommand moveUpCommand = new ApplyNormalForceCommand(_bodySprite, new NormalForce(-_amount * Vector2.UnitY));
            commandBindings.Add(Keyboard.Key.Up, moveUpCommand);

            ApplyNormalForceCommand moveDownCommand = new ApplyNormalForceCommand(_bodySprite, new NormalForce(_amount * Vector2.UnitY));
            commandBindings.Add(Keyboard.Key.Down, moveDownCommand);

            ApplyNormalForceCommand moveLeftCommand = new ApplyNormalForceCommand(_bodySprite, new NormalForce(-_amount * Vector2.UnitX));
            commandBindings.Add(Keyboard.Key.Left, moveLeftCommand);

            ApplyNormalForceCommand moveRightCommand = new ApplyNormalForceCommand(_bodySprite, new NormalForce(_amount * Vector2.UnitX));
            commandBindings.Add(Keyboard.Key.Right, moveRightCommand);

            return commandBindings;
        }
    }
}
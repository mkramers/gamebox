﻿using System;
using System.Collections.Generic;
using SFML.Window;

namespace RenderCore
{
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
}
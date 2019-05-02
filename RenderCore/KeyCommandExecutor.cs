using System.Collections.Generic;
using System.Windows.Input;
using SFML.Window;

namespace RenderCore
{
    public class KeyCommandExecutor : IKeyHandler
    {
        private readonly Dictionary<Keyboard.Key, ICommand> m_keyBindings;

        public KeyCommandExecutor(Dictionary<Keyboard.Key, ICommand> _keyBindings)
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
}
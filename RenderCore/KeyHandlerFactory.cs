using System.Collections.Generic;
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
}
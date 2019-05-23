using System.Collections.Generic;
using SFML.Window;

namespace RenderCore
{
    public static class KeyHandlerFactory
    {
        public static KeyHandler CreateEntityKeyHandler(IEntity _manEntity, float _force)
        {
            Dictionary<Keyboard.Key, IKeyCommand>
                moveCommands = KeyCommandsFactory.GetMovementCommands(_manEntity, _force);
            KeyHandler moveExecutor = CreateKeyHandler(moveCommands);
            return moveExecutor;
        }

        private static KeyHandler CreateKeyHandler(Dictionary<Keyboard.Key, IKeyCommand> _keyCommands)
        {
            KeyStateContainer keyStateContainer = new KeyStateContainer();
            return new KeyHandler(_keyCommands, keyStateContainer);
        }
    }
}
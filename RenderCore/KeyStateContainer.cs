using System.Collections.Generic;
using SFML.Window;

namespace RenderCore
{
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
}
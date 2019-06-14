using SFML.Window;

namespace GameCore.Input.Key
{
    public interface IKeyStateContainer
    {
        void Update(Keyboard.Key _key);
        KeyState GetKeyState(Keyboard.Key _key);
    }
}
using SFML.Window;

namespace RenderCore
{
    public interface IKeyStateContainer
    {
        void Update(Keyboard.Key _key);
        KeyState GetKeyState(Keyboard.Key _key);
    }
}
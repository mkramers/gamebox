namespace RenderCore.Input.Key
{
    public class KeyState
    {
        public KeyState()
        {
            IsPressed = false;
            PreviousIsPressed = false;
        }

        public bool IsPressed { get; private set; }
        public bool PreviousIsPressed { get; private set; }

        public void Update(bool _isPressed)
        {
            PreviousIsPressed = IsPressed;
            IsPressed = _isPressed;
        }
    }
}
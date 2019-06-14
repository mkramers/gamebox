using System.Windows.Input;

namespace GameCore.Input.Key
{
    public interface IKeyCommand : ICommand
    {
        bool CanExecute(KeyState _keyState);

        // ReSharper disable once UnusedParameter.Global
        void Execute(KeyState _keyState);
    }
}
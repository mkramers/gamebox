using System.Windows.Input;

namespace RenderCore
{
    public interface IKeyCommand : ICommand
    {
        bool CanExecute(KeyState _keyState);
        // ReSharper disable once UnusedParameter.Global
        void Execute(KeyState _keyState);
    }
}
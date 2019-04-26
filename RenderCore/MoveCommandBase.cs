using System;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class MoveCommandBase : IMoveCommand
    {
        protected readonly Transformable m_transformable;

        protected MoveCommandBase(Transformable _transformable)
        {
            m_transformable = _transformable;
        }

        public abstract bool CanExecute(object _parameter);

        public abstract void Execute(object _parameter);

        public event EventHandler CanExecuteChanged;
    }
}
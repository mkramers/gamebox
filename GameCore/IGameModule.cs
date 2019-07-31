using System;
using Common.Tickable;

namespace GameCore
{
    public interface IGameModule : ITickable
    {
        event EventHandler PauseGame;
        event EventHandler ResumeGame;
    }
}
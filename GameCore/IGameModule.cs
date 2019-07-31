using System;
using System.Collections.Generic;
using Common.Tickable;
using TGUI;

namespace GameCore
{
    public interface IGameModule : ITickable
    {
        IEnumerable<Widget> GetGuiWidgets();

        event EventHandler PauseGame;
        event EventHandler ResumeGame;
    }
}
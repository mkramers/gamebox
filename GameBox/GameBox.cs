using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using RenderCore;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameBox
{
    public class GameBox : Game
    {
        public GameBox(string _windowTitle, Vector2u _windowSize) : base(_windowTitle, _windowSize)
        {
        }

        public override void CreateMainCharacter()
        {
            const float mass = 1.0f;
            ManEntityFactory manEntityFactory = new ManEntityFactory();
            IEntity man = manEntityFactory.GetMan(mass, m_entityPhysics);

            m_entityPhysics.Add(man);

            Dictionary<Keyboard.Key, ICommand> moveCommands = KeyCommandsFactory.GetBodySpriteCommands(man, 1.0f);
            KeyCommandExecuter moveExecutor = new KeyCommandExecuter(moveCommands);

            m_renderCoreWindow.ClearKeyHandlers();
            m_renderCoreWindow.AddKeyHandler(moveExecutor);

            m_renderCoreWindow.Add(man.GetDrawable());
        }
    }
}
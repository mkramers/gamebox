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
            const float mass = 0.1f;
            EntityFactory entityFactory = new EntityFactory();
            IEntity man = entityFactory.CreateEntity(mass, m_entityPhysics, ResourceId.MAN);

            m_entityPhysics.Add(man);
            m_renderCoreWindow.Add(man);
            
            const int range = 20;
            for (int i = 0; i < range; i++)
            {
                Vector3 position = new Vector3(-range/2 + i, 5, 0);
                IEntity wood = entityFactory.CreateEntity(1, m_entityPhysics, ResourceId.WOOD);
                m_renderCoreWindow.Add(wood);
            }
            
            Dictionary<Keyboard.Key, ICommand> moveCommands = KeyCommandsFactory.GetBodySpriteCommands(man, 1.0f);
            KeyCommandExecuter moveExecutor = new KeyCommandExecuter(moveCommands);

            m_renderCoreWindow.ClearKeyHandlers();
            m_renderCoreWindow.AddKeyHandler(moveExecutor);
        }
    }
}
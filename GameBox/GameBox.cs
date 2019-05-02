using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using Aether.Physics2D.Dynamics;
using RenderCore;
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
            IEntity man = EntityFactory.CreateEntity(mass, -5 * Vector2.UnitY, m_entityPhysics, ResourceId.MAN,
                BodyType.Dynamic);

            m_entityPhysics.Add(man);
            m_renderCoreWindow.Add(man);

            const int range = 20;
            for (int i = 0; i < range; i++)
            {
                Vector2 position = new Vector2(-range / 2 + i, 5);
                IEntity wood =
                    EntityFactory.CreateEntity(1, position, m_entityPhysics, ResourceId.WOOD, BodyType.Static);
                m_entityPhysics.Add(wood);
                m_renderCoreWindow.Add(wood);
            }

            Dictionary<Keyboard.Key, ICommand> moveCommands = KeyCommandsFactory.GetBodySpriteCommands(man, 0.5f);
            KeyCommandExecuter moveExecutor = new KeyCommandExecuter(moveCommands);

            m_renderCoreWindow.ClearKeyHandlers();
            m_renderCoreWindow.AddKeyHandler(moveExecutor);
        }
    }
}
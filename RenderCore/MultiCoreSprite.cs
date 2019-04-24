using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
    public class MultiCoreSprite : IBodyRepresentation
    {
        private readonly IEnumerable<CoreSprite> m_sprites;

        public MultiCoreSprite(IEnumerable<CoreSprite> _sprites)
        {
            m_sprites = _sprites;
        }

        public void Draw(RenderTarget _renderTarget)
        {
            foreach (CoreSprite sprite in m_sprites)
            {
                sprite.Draw(_renderTarget);
            }
        }
    }
}
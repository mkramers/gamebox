using SFML.Graphics;

namespace RenderCore
{
    public class CoreSprite : IRenderable
    {
        private readonly Sprite m_sprite;

        public CoreSprite(Sprite _sprite)
        {
            m_sprite = _sprite;
        }

        public void Draw(RenderTarget _renderTarget)
        {
            m_sprite.Draw(_renderTarget, RenderStates.Default);
        }
    }
}
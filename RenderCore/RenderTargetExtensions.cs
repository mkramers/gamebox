using SFML.Graphics;

namespace RenderCore
{
    public static class RenderTargetExtensions
    {
        public static void Draw(this RenderTarget _target, Texture _texture, RenderStates _states)
        {
            Sprite sceneSprite = new Sprite(_texture);
            _target.Draw(sceneSprite, _states);
        }
    }
}
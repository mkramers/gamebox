using SFML.Graphics;

namespace RenderCore
{
    public static class RenderTargetExtensions
    {
        public static void Draw(this RenderTarget _target, RenderTexture _source, RenderStates _states)
        {
            Sprite sceneSprite = new Sprite(_source.Texture);
            _target.Draw(sceneSprite, _states);
        }
    }
}
using SFML.Graphics;

namespace RenderCore
{
    public interface IViewController : ITickable
    {
        View GetView();
    }
}
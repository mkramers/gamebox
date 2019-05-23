using SFML.Graphics;

namespace RenderCore
{
    public class ViewProviderBase : View, IViewProvider
    {
        public ViewProviderBase(View _copy) : base(_copy)
        {
        }

        public ViewProviderBase()
        {
        }

        public View GetView()
        {
            return this;
        }
    }
}
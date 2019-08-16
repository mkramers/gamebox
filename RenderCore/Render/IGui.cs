using System.Collections.Generic;
using RenderCore.Widget;
using SFML.Graphics;

namespace RenderCore.Render
{
    public interface IGui
    {
        void Add(IGuiWidget _widget);
        void Remove(IGuiWidget _widget);
        void SetView(View _view);
        void Draw();
        IEnumerable<IGuiWidget> GetWidgets();
    }
}
namespace RenderCore.Drawable
{
    public interface IWidgetConsumer
    {
        void AddWidgetProvider(IWidgetProvider _widgetProvider);
        void RemoveWidgetProvider(IWidgetProvider _widgetProvider);
    }
}
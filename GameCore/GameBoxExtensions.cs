using System.Numerics;
using Common.Tickable;
using LibExtensions;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Widget;
using SFML.System;
using TGUI;

namespace GameCore
{
    public static class GameBoxExtensions
    {
        public static void AddTickable(this IGameBox _gameBox, ITickable _tickable)
        {
            TickableProvider tickableProvider = new TickableProvider(_tickable);
            _gameBox.AddTickableProvider(tickableProvider);
        }

        private static void AddWidget(this IGameBox _gameBox, IGuiWidget _widget)
        {
            WidgetProvider widgetProvider = new WidgetProvider(_widget);
            _gameBox.AddWidgetProvider(widgetProvider);
        }

        public static void AddFpsWidget(this IGameBox _gameBox)
        {
            WidgetFontSettings widgetFontSettingsFactory = new WidgetFontSettings();
            FontSettings fpsFontSettings = widgetFontSettingsFactory.GetSettings(WidgetFontSettingsType.FPS_COUNTER);
            
            FpsLabel fpsLabel = new FpsLabel(5, fpsFontSettings);

            GuiWidget widget = new GuiWidget(fpsLabel, new Vector2(0.2f, 0.98f));

            _gameBox.AddWidget(widget);
            _gameBox.AddTickable(fpsLabel);
        }
    }
}
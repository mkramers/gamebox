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

        private static void AddWidget(this IGameBox _gameBox, Widget _widget)
        {
            WidgetProvider widgetProvider = new WidgetProvider(_widget);
            _gameBox.AddWidgetProvider(widgetProvider);
        }

        public static void AddFpsWidget(this IGameBox _gameBox)
        {
            WidgetFontSettings widgetFontSettingsFactory = new WidgetFontSettings();
            FontSettings fpsFontSettings = widgetFontSettingsFactory.GetSettings(WidgetFontSettingsType.FPS_COUNTER);

            Vector2u windowSize = _gameBox.GetWindowSize();

            Vector2 textPosition = new Vector2(100, 10);

            FpsLabel fpsWidget = new FpsLabel(5, fpsFontSettings)
            {
                Position = textPosition.GetVector2F()
            };

            _gameBox.AddWidget(fpsWidget);
            _gameBox.AddTickable(fpsWidget);
        }
    }
}
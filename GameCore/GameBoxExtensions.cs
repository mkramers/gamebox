﻿using System.Numerics;
using Common.Tickable;
using GameCore.Entity;
using LibExtensions;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Render;
using RenderCore.Widget;
using SFML.System;
using TGUI;

namespace GameCore
{
    public static class GameBoxExtensions
    {
        public static void AddDrawable(this IGameBox _gameBox, IDrawable _drawable)
        {
            DrawableProvider drawableProvider = new DrawableProvider(_drawable);
            _gameBox.AddDrawableProvider(drawableProvider);
        }

        public static void AddTickable(this IGameBox _gameBox, ITickable _tickable)
        {
            TickableProvider tickableProvider = new TickableProvider(_tickable);
            _gameBox.AddTickableProvider(tickableProvider);
        }

        public static void AddFpsWidget(this IGameBox _gameBox)
        {
            WidgetFontSettings widgetFontSettingsFactory = new WidgetFontSettings();
            FontSettings fpsFontSettings = widgetFontSettingsFactory.GetSettings(WidgetFontSettingsType.FPS_COUNTER);

            Vector2u windowSize = _gameBox.GetWindowSize();

            Vector2 textPosition = new Vector2(windowSize.X / 2.0f, windowSize.Y - fpsFontSettings.Size);

            FpsLabel fpsWidget = new FpsLabel(5, fpsFontSettings)
            {
                Position = textPosition.GetVector2F()
            };

            Gui gui = _gameBox.GetGui();
            gui.Add(fpsWidget);

            _gameBox.AddTickable(fpsWidget);
        }

        public static void AddEntity(this IGameBox _gameBox, IEntity _entity)
        {
            _gameBox.AddTickable(_entity);
            _gameBox.AddDrawable(_entity);
        }
    }
}
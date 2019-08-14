﻿using System;
using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.System;
using TGUI;

namespace GameCore
{
    public interface IGameBox : ILoopable, IDisposable
    {
        void AddDrawableProvider(IDrawableProvider _drawableProvider);
        void AddWidgetProvider(IWidgetProvider _widgetProvider);
        void SetViewProvider(IViewProvider _viewProvider);
        void SetIsPaused(bool _isPaused);
        Gui GetGui();
        Vector2u GetWindowSize();
        void AddTickableProvider(ITickableProvider _tickableProvider);
    }
}
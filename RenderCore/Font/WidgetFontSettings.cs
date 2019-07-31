using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore.Font
{
    public class WidgetFontSettings : FontSettingsFactory<WidgetFontSettingsType>
    {
        public WidgetFontSettings() : base(new Dictionary<WidgetFontSettingsType, FontSettings>())
        {
            {
                FontSettings settings = FontSettingsExtensions.GetFontSettings(FontId.ROBOTO, 1.0f, 14, Color.Red);
                AddSettings(WidgetFontSettingsType.FPS_COUNTER, settings);
            }
            {
                FontSettings settings = FontSettingsExtensions.GetFontSettings(FontId.ROBOTO, 0.5f, 72, Color.Green);
                AddSettings(WidgetFontSettingsType.LABELED_GRID, settings);
            }
        }
    }
}
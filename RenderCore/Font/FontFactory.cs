using System.Collections.Generic;
using System.Diagnostics;

namespace RenderCore.Font
{
    public class FontSettings
    {
        public FontSettings(float _scale, uint _size, SFML.Graphics.Font _font)
        {
            Scale = _scale;
            Size = _size;
            Font = _font;
        }

        public float Scale { get; }
        public uint Size { get; }
        public SFML.Graphics.Font Font { get; }
    }

    public static class FontSettingsExtensions
    {
        public static FontSettings GetFontSettings(FontId _fontId, float _scale, uint _size)
        {
            FontFactory fontFactory = new FontFactory();
            SFML.Graphics.Font font = fontFactory.GetFont(FontId.ROBOTO);
            return new FontSettings(_scale, _size, font);
        }
    }

    public enum WidgetFontSettingsType
    {
        FPS_COUNTER,
    }

    public class WidgetFontSettings : FontSettingsFactory<WidgetFontSettingsType>
    {
        public WidgetFontSettings() : base(new Dictionary<WidgetFontSettingsType, FontSettings>())
        {
            {
                FontSettings settings = FontSettingsExtensions.GetFontSettings(FontId.ROBOTO, 0.02f, 72);
                AddSettings(WidgetFontSettingsType.FPS_COUNTER, settings);
            }
        }
    }

    public class FontSettingsFactory<T> where T : System.Enum
    {
        private readonly Dictionary<T, FontSettings> m_fontSettings;

        protected FontSettingsFactory(Dictionary<T, FontSettings> _fontSettings)
        {
            m_fontSettings = _fontSettings;
        }

        public FontSettings GetSettings(T _id)
        {
            Debug.Assert(m_fontSettings.ContainsKey(_id));

            return m_fontSettings[_id];
        }

        protected void AddSettings(T _id, FontSettings _fontSettings)
        {
            Debug.Assert(!m_fontSettings.ContainsKey(_id));

            m_fontSettings.Add(_id, _fontSettings);
        }
    }

    public class FontFactory
    {
        private readonly Dictionary<FontId, string> m_fontResourceMappings;

        public FontFactory()
        {
            m_fontResourceMappings = new Dictionary<FontId, string>
            {
                {FontId.ROBOTO, "RenderCore.Resources.Roboto-Regular.ttf"}
            };
        }

        public SFML.Graphics.Font GetFont(FontId _fontId)
        {
            string fontPath = m_fontResourceMappings[_fontId];

            byte[] resourceData = Resource.ResourceUtilities.GetResourceData(fontPath);

            return new SFML.Graphics.Font(resourceData);
        }
    }
}
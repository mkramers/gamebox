using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RenderCore.Font
{
    public class FontSettingsFactory<T> where T : Enum
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
}
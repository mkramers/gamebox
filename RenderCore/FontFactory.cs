using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
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

        public Font GetFont(FontId _fontId)
        {
            string fontPath = m_fontResourceMappings[_fontId];

            byte[] resourceData = ResourceUtilities.GetResourceData(fontPath);

            return new Font(resourceData);
        }
    }
}
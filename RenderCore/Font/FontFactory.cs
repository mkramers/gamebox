using System.Collections.Generic;

namespace RenderCore.Font
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

        public SFML.Graphics.Font GetFont(FontId _fontId)
        {
            string fontPath = m_fontResourceMappings[_fontId];

            byte[] resourceData = Resource.ResourceUtilities.GetResourceData(fontPath);

            return new SFML.Graphics.Font(resourceData);
        }
    }
}
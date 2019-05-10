using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
                {FontId.ROBOTO, "RenderCore.Resources.Roboto-Regular.ttf"},
            };
        }

        public Font GetFont(FontId _fontId)
        {
            string fontPath = m_fontResourceMappings[_fontId];

            byte[] resourceData = ResourceUtilities.GetResourceData(fontPath);

            return new Font(resourceData);
        }
    }

    public enum FontId
    {
        ROBOTO,
    }

    public static class ResourceUtilities
    {
        public static byte[] GetResourceData(string _resourceName)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream resourceStream = myAssembly.GetManifestResourceStream(_resourceName);
            Debug.Assert(resourceStream != null);

            byte[] resourceData;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                resourceStream.CopyTo(memoryStream);
                resourceData = memoryStream.ToArray();
            }

            return resourceData;
        }
    }

    public class ResourceFactory
    {
        private readonly Dictionary<ResourceId, TextureMetaInfo> m_textureMetaInfo;

        public ResourceFactory()
        {
            m_textureMetaInfo = new Dictionary<ResourceId, TextureMetaInfo>
            {
                {ResourceId.MAN, new TextureMetaInfo("RenderCore.Resources.man.png", new IntRect(50, 24, 24, 24))},
                {ResourceId.WOOD, new TextureMetaInfo("RenderCore.Resources.wood.bmp")}
            };
        }

        public Texture GetTexture(ResourceId _resourceId)
        {
            TextureMetaInfo resourceMeta = m_textureMetaInfo[_resourceId];

            byte[] resourceData = ResourceUtilities.GetResourceData(resourceMeta.ResourceName);

            Image image = new Image(resourceData);

            Texture texture = resourceMeta.TextureCropRect.HasValue
                ? new Texture(image, resourceMeta.TextureCropRect.Value)
                : new Texture(image);

            return texture;
        }
    }
}
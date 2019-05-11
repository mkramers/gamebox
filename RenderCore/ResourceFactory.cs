using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
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
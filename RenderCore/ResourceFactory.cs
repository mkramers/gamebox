using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
    public static class ResourceFactory
    {
        private static readonly Dictionary<ResourceId, TextureResourceId> m_textureResources;

        static ResourceFactory()
        {
            m_textureResources = new Dictionary<ResourceId, TextureResourceId>
            {
                {ResourceId.MAN, new TextureResourceId("RenderCore.Resources.man.png", new IntRect(50, 24, 24, 24))},
                {ResourceId.MK, new TextureResourceId("RenderCore.Resources.head-mk_0.png")},
                {ResourceId.WOOD, new TextureResourceId("RenderCore.Resources.wood.bmp")}
            };
        }

        public static Texture GetTexture(ResourceId _resourceId)
        {
            TextureResourceId resourceMeta = m_textureResources[_resourceId];
            
            Texture texture = TextureCache.Instance.GetTextureFromResource(resourceMeta.ResourceName, resourceMeta.Area);

            return texture;
        }
    }
}
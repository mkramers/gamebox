﻿using System.Collections.Generic;
using RenderCore.TextureCache;
using SFML.Graphics;

namespace RenderCore.Resource
{
    public static class ResourceFactory
    {
        private static readonly Dictionary<ResourceId, TextureResourceArgs> m_textureResources;

        static ResourceFactory()
        {
            m_textureResources = new Dictionary<ResourceId, TextureResourceArgs>
            {
                {ResourceId.MAN, new TextureResourceArgs("RenderCore.Resources.man.png", new IntRect(50, 24, 24, 24))},
                {ResourceId.MK, new TextureResourceArgs("RenderCore.Resources.head-mk_0.png")},
                {ResourceId.WOOD, new TextureResourceArgs("RenderCore.Resources.wood.bmp")}
            };
        }

        public static Texture GetTexture(ResourceId _resourceId)
        {
            TextureResourceArgs resourceMeta = m_textureResources[_resourceId];

            Texture texture =
                TextureCache.TextureCache.Instance.GetTextureFromResource(resourceMeta.ResourceName, resourceMeta.Area);

            return texture;
        }
    }
}
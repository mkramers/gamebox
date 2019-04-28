using System.Diagnostics;
using System.IO;
using System.Reflection;
using SFML.Graphics;

namespace RenderCore
{
    public class ResourceFactory
    {
        public Texture GetTexture(ResourceId _resourceId)
        {
            string resourceName = "RenderCore.Resources.man.png";

            byte[] resourceData = GetResourceData(resourceName);

            Image image = new Image(resourceData);

            IntRect textureCrop = new IntRect(50, 24, 24, 24);

            Texture texture = new Texture(image, textureCrop);
            return texture;
        }

        private static byte[] GetResourceData(string _resourceName)
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
}
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

            Texture texture = new Texture(resourceData);
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
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RenderCore
{
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
}
using ResourceUtilities.Aseprite;

namespace GameBox
{
    internal static class Program
    {
        private static void Main()
        {
            //const string windowTitle = "GameBox";
            //Vector2u windowSize = new Vector2u(800, 400);
            //const float aspectRatio = 1.0f;

            //Vector2 gravity = new Vector2(0, 9);
            //GameBox gameBox = new GameBox(windowTitle, windowSize, gravity, aspectRatio);
            //gameBox.StartLoop();
            //gameBox.Dispose();

            string path = @"C:\dev\GameBox\RenderCore\Resources\art\sample_tree_map.json";

            IFileLoader fileLoader = new FileLoader();
            File file = fileLoader.LoadFromFile(path);
        }
    }
}
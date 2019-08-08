namespace RenderBox
{
    internal static class Program
    {
        private static void Main()
        {
            const float aspectRatio = 1.0f;

            //RenderBox renderBox = new RenderBox("RenderBox", new Vector2u(800, 800), aspectRatio);
            //renderBox.StartLoop();

            SubmitToDrawRenderBox renderBox = new SubmitToDrawRenderBox();
            renderBox.StartLoop();
        }
    }
}
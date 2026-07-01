namespace RayTracer.Cli;

public class TerminalGraphics
{
    // prints a 100 x 100 pixel rendering of the scene to the console
    public static void PrintScene(Scene _scene)
    {
        Camera previewCam = new(100, 100, _scene.Camera.FieldOfView);
        previewCam.Transform = _scene.Camera.Transform;
        Canvas canvas = previewCam.Render(_scene.World);
        int lineLen = 0;

        for (int row = 0; row < canvas.Width; row++)
        {
            for (int col = 0; col < canvas.Height; col++)
            {
                Color c = canvas._pixelMatrix[col, row];
                int r = (int)(c.X * 255);
                int g = (int)(c.Y * 255);
                int b = (int)(c.Z * 255);
                Console.Write($"\u001b[48;2;{r};{g};{b}m  \u001b[0m");

                lineLen++;
                //print new line when the row of pixels finsishes
                if (lineLen >= previewCam.VSize)
                {
                    Console.Write('\n');
                    lineLen = 0;
                }
            }
        }
    }
}

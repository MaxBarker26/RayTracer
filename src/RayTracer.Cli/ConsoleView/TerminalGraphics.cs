namespace RayTracer.Cli;

public class TerminalGraphics
{
    // prints a 100 x 100 pixel rendering of the scene to the console
    public static void PrintScene(Scene _scene)
    {
        Camera previewCam = new(100, 50, _scene.Camera.FieldOfView);
        previewCam.Transform = _scene.Camera.Transform;
        Canvas canvas = previewCam.Render(_scene.World);

        for (int row = 0; row < canvas.Height; row += 2)
        {
            for (int col = 0; col < canvas.Width; col++)
            {
                Color cTop = canvas._pixelMatrix[col, row];
                int r1 = Math.Clamp((int)(cTop.X * 255), 0, 255);
                int g1 = Math.Clamp((int)(cTop.Y * 255), 0, 255);
                int b1 = Math.Clamp((int)(cTop.Z * 255), 0, 255);

                int r2 = 0,
                    g2 = 0,
                    b2 = 0;
                if (row + 1 < canvas.Height)
                {
                    Color cBottom = canvas._pixelMatrix[col, row + 1];
                    r2 = Math.Clamp((int)(cBottom.X * 255), 0, 255);
                    g2 = Math.Clamp((int)(cBottom.Y * 255), 0, 255);
                    b2 = Math.Clamp((int)(cBottom.Z * 255), 0, 255);
                }

                // \u001b[38;2;... sets top color (foreground)
                // \u001b[48;2;... sets bottom color (background)
                // '▀' is the unicode half-block so pixels can be one character wide
                Console.Write($"\u001b[38;2;{r1};{g1};{b1}m\u001b[48;2;{r2};{g2};{b2}m▀");
            }

            Console.Write("\u001b[0m\n");
        }
    }
}

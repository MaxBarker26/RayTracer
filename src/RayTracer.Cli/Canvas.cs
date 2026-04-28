namespace RayTracer.Cli;

public class Canvas
{
    public Color[,] _pixelMatrix;
    public int Width { get; }
    public int Height { get; }

    public Canvas(int width, int height)
    {
        Width = width;
        Height = height;
        _pixelMatrix = new Color[width, height];
        // Initialize all pixels to black
        SetAllPixels(new Color(0, 0, 0));
    }

    public void SetAllPixels(Color color)
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _pixelMatrix[i, j] = color;
            }
        }
    }

    public void SetPixel(int x, int y, Color color)
    {
        _pixelMatrix[x, y] = color;
    }

    public Color GetPixel(int x, int y)
    {
        return _pixelMatrix[x, y];
    }
}

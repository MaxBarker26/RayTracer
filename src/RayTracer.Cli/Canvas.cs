namespace RayTracer.Cli;

public class Canvas
{
    public Color[,] _pixelMatrix;
    public int Width { get; }
    public int Height { get; }
    public int MaxColor { get; } = 255;

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
        if (x < Width && y < Height && y > 0)
        {
            _pixelMatrix[x, y] = color;
        }
    }

    public Color GetPixel(int x, int y)
    {
        return _pixelMatrix[x, y];
    }

    public string SavePPM()
    {
        StringWriter data = new();
        data.WriteLine("P3");
        data.WriteLine(Width + " " + Height);
        data.WriteLine("255");

        for (int i = 0; i < Height; i++)
        {
            int currentLineLength = 0;
            for (int j = 0; j < Width; j++)
            {
                string colorString = ConvertColorToPPM(_pixelMatrix[j, i]);
                if ((currentLineLength + colorString.Length) <= 70)
                {
                    data.Write(colorString);
                    currentLineLength += colorString.Length;
                }
                else
                {
                    // removes the last space character at the end of a line.
                    data.GetStringBuilder().Remove(data.GetStringBuilder().Length - 1, 1);
                    data.Write("\n" + colorString);
                    currentLineLength = colorString.Length;
                }
            }

            // removes the last space character at the end of a line.
            data.GetStringBuilder().Remove(data.GetStringBuilder().Length - 1, 1);

            data.Write("\n");
        }
        return data.ToString();
    }

    private string ConvertColorToPPM(Color color)
    {
        string ppmColor = "";
        if (color.X >= 1)
        {
            ppmColor += MaxColor + " ";
        }
        else if (color.X <= 0)
        {
            ppmColor += 0 + " ";
        }
        else
        {
            ppmColor += Math.Round(MaxColor * color.X) + " ";
        }

        if (color.Y >= 1)
        {
            ppmColor += MaxColor + " ";
        }
        else if (color.Y <= 0)
        {
            ppmColor += 0 + " ";
        }
        else
        {
            ppmColor += Math.Round(MaxColor * color.Y) + " ";
        }

        if (color.Z >= 1)
        {
            ppmColor += MaxColor + " ";
        }
        else if (color.Z <= 0)
        {
            ppmColor += 0 + " ";
        }
        else
        {
            ppmColor += Math.Round(MaxColor * color.Z) + " ";
        }

        return ppmColor;
    }
}

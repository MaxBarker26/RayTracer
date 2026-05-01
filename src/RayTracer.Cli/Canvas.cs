namespace RayTracer.Cli;

/// <summary>
/// Represents a canvas for drawing and manipulating pixels, capable of being saved to a PPM image format.
/// </summary>
public class Canvas
{
    /// <summary>
    /// The internal 2D array representing the pixel matrix of the canvas.
    /// </summary>
    public Color[,] _pixelMatrix;

    /// <summary>
    /// Gets the width of the canvas in pixels.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of the canvas in pixels.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the maximum color value used for PPM output, typically 255.
    /// </summary>
    public int MaxColor { get; } = 255;

    /// <summary>
    /// Initializes a new instance of the <see cref="Canvas"/> class with the specified width and height.
    /// All pixels are initialized to black.
    /// </summary>
    /// <param name="width">The width of the canvas.</param>
    /// <param name="height">The height of the canvas.</param>
    public Canvas(int width, int height)
    {
        Width = width;
        Height = height;
        _pixelMatrix = new Color[width, height];
        // Initialize all pixels to black
        SetAllPixels(new Color(0, 0, 0));
    }

    /// <summary>
    /// Sets all pixels on the canvas to a specified color.
    /// </summary>
    /// <param name="color">The color to set all pixels to.</param>
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

    /// <summary>
    /// Sets the color of a specific pixel at the given coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the pixel (column).</param>
    /// <param name="y">The y-coordinate of the pixel (row).</param>
    /// <param name="color">The color to set the pixel to.</param>
    public void SetPixel(int x, int y, Color color)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            _pixelMatrix[x, y] = color;
        }
    }

    /// <summary>
    /// Gets the color of a pixel at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the pixel (column).</param>
    /// <param name="y">The y-coordinate of the pixel (row).</param>
    /// <returns>The <see cref="Color"/> of the pixel at the given coordinates.</returns>
    public Color GetPixel(int x, int y)
    {
        // Assuming valid coordinates are always passed or handled externally.
        // A robust implementation might add boundary checks and return a default color or throw an exception.
        return _pixelMatrix[x, y];
    }

    /// <summary>
    /// Saves the current canvas content to a PPM (Portable Pixmap) string format.
    /// The PPM output is formatted with a maximum line length of 70 characters.
    /// </summary>
    /// <returns>A string representing the canvas in PPM image format.</returns>
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
            // This applies if the last line of colors ended exactly at or near 70 chars, or if it's the last pixel in the row.
            if (
                data.GetStringBuilder().Length > 0
                && data.GetStringBuilder()[data.GetStringBuilder().Length - 1] == ' '
            )
            {
                data.GetStringBuilder().Remove(data.GetStringBuilder().Length - 1, 1);
            }

            data.Write("\n");
        }
        return data.ToString();
    }

    /// <summary>
    /// Converts a <see cref="Color"/> object to its string representation for PPM format.
    /// Each color component (R, G, B) is scaled by <see cref="MaxColor"/> and rounded to the nearest integer,
    /// then appended with a space.
    /// </summary>
    /// <param name="color">The <see cref="Color"/> object to convert.</param>
    /// <returns>A space-separated string of the R, G, B integer values for the PPM file.</returns>
    private string ConvertColorToPPM(Color color)
    {
        string ppmColor = "";
        // Red component
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

        // Green component
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

        // Blue component
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

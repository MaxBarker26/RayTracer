namespace RayTracer.Tests;

using RayTracer.Cli;

public class CanvasTests()
{
    [Fact]
    public void CanvasConstructor_WidthAndHeightAreSetWithConstructor_AreAsExpected()
    {
        Canvas canvas = new Canvas(10, 20);
        Assert.Equal(10, canvas.Width);
        Assert.Equal(20, canvas.Height);
    }

    [Fact]
    public void CanvasConstructor_PixelColorsAreInitializedInConstructor_AllPixelsAreBlack()
    {
        Canvas canvas = new Canvas(10, 20);
        for (int i = 0; i < canvas.Width; i++)
        {
            for (int j = 0; j < canvas.Width; j++)
            {
                Assert.Equal(new Color(0, 0, 0), canvas.GetPixel(i, j));
            }
        }
    }

    [Fact]
    public void SetPixel_SetASpecificPixelToASpecificColor_PixelIsAsExpected()
    {
        Canvas canvas = new Canvas(10, 20);
        Color white = new Color(1, 1, 1);
        canvas.SetPixel(3, 10, white);

        Assert.Equal(white, canvas.GetPixel(3, 10));
    }

    [Fact]
    public void SavePPM_PPMHeaderIsInCorrectFormat()
    {
        Canvas c = new(10, 20);
        String ppm = c.SavePPM();
        String[] ppmLines = ppm.Split("\n");
        Assert.Equal("P3", ppmLines[0]);
        Assert.Equal("10 20", ppmLines[1]);
        Assert.Equal("255", ppmLines[2]);
    }

    [Fact]
    public void SavePPM_PPMStringBody_PixelsScaleCorrectly()
    {
        Canvas c = new(2, 3);
        c.SetPixel(1, 0, new(1.5, 0, 0));
        c.SetPixel(0, 1, new(0, 0.5, 0));
        c.SetPixel(1, 2, new(-0.3, 0, 1));
        String comparison1 = "0 0 0 255 0 0";
        String comparison2 = "0 128 0 0 0 0";
        String comparison3 = "0 0 0 0 0 255";
        String ppm = c.SavePPM();
        String[] ppmLines = ppm.Split("\n");
        Assert.Equal(comparison1, ppmLines[3]);
        Assert.Equal(comparison2, ppmLines[4]);
        Assert.Equal(comparison3, ppmLines[5]);
    }
}

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
}

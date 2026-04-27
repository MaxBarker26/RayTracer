namespace RayTracer.Tests;

using RayTracer.Cli;

public class CanvasTests()
{
    [Fact]
    public void WidthAndHeight_WidthAndHeightAreSetWithConstructor_AreAsExpected()
    {
        Canvas canvas = new Canvas(10, 20);
        Assert.Equal(10, canvas.Width);
        Assert.Equal(20, canvas.Height);
    }
}

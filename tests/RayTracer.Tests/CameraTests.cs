namespace RayTracer.Tests;

using RayTracer.Cli;

public class CameraTests()
{
    [Fact]
    public void CameraConstructor()
    {
        Camera c = new(160, 120, Math.PI / 2);

        Assert.Equal(160, c.HSize);
        Assert.Equal(120, c.VSize);
        Assert.Equal(Math.PI / 2, c.FieldOfView);
        Assert.Equal(Matrix.Identity(), c.Transform);
    }

    [Fact]
    public void PixelSize_PixelSizeForHorizontalCanvas_EqualsExpected()
    {
        Camera c = new(200, 125, Math.PI / 2);

        Assert.Equal(0.01, c.PixelSize, 0.0001);
    }

    [Fact]
    public void PixelSize_PixelSizeForVerticalCanvas_EqualsExpected()
    {
        Camera c = new(125, 200, Math.PI / 2);

        Assert.Equal(0.01, c.PixelSize, 0.0001);
    }
}

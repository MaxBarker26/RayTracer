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

    [Fact]
    public void RayForPixel_RayThroughCenterOfCanvas_RayOriginAndDirectionAreAsExpected()
    {
        Camera c = new(201, 101, Math.PI / 2);
        Ray r = c.RayForPixel(100, 50);
        Assert.Equal(new(0, 0, 0), r.Origin);
        Assert.Equal(new(0, 0, -1), r.Direction);
    }

    [Fact]
    public void RayForPixel_RayThroughCornerOfCanvas_RayOriginAndDirectionAreAsExpected()
    {
        Camera c = new(201, 101, Math.PI / 2);
        Ray r = c.RayForPixel(0, 0);
        Assert.Equal(new(0, 0, 0), r.Origin);
        Assert.Equal(new(0.66519, 0.33259, -0.66851), r.Direction);
    }

    [Fact]
    public void RayForPixel_RayWhenCameraIsTransformed_RayOriginAndDirectionAreAsExpected()
    {
        Camera c = new(201, 101, Math.PI / 2);
        c.Transform = Matrix.RotationY(Math.PI / 4) * Matrix.Translation(0, -2, 5);
        Ray r = c.RayForPixel(100, 50);
        Assert.Equal(new(0, 2, -5), r.Origin);
        Assert.Equal(new(Math.Sqrt(2) / 2, 0, -Math.Sqrt(2) / 2), r.Direction);
    }

    [Fact]
    public void Render_RenderingAWorldWithACamera_ImagePixelIsExpected()
    {
        World w = World.Default();
        Camera c = new(11, 11, Math.PI / 2);
        Point from = new(0, 0, -5);
        Point to = new(0, 0, 0);
        Vector up = new(0, 1, 0);
        c.Transform = Matrix.View(from, to, up);
        Canvas image = c.Render(w);
        Assert.Equal(new Color(0.38066, 0.47583, 0.2855), image.GetPixel(5, 5));
    }
}

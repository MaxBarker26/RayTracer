namespace RayTracer.Tests;

using RayTracer.Cli;

public class PointLightTests
{
    [Fact]
    public void Constructor_ConstructorFillsClassPropertiesCorrectly()
    {
        Color intensity = new(1, 1, 1);
        Point position = new(0, 0, 0);
        PointLight light = new(position, intensity);
        Assert.Equal(intensity, light.Intensity);
    }
}

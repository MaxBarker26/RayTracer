namespace RayTracer.Tests;

using RayTracer.Cli;

public class MaterialTests
{
    [Fact]
    public void DefaultConstructor_ConstructsDefaultMaterial()
    {
        Material m = new();
        Assert.Equal(new Color(1, 1, 1), m.Color);
        Assert.Equal(0.1, m.Ambient);
        Assert.Equal(0.9, m.Diffuse);
        Assert.Equal(0.9, m.Specular);
        Assert.Equal(200, m.Shininess);
    }

    [Fact]
    public void SphereHasDefaultMaterial()
    {
        Sphere s = new();
        Assert.Equal(new Material(), s.Material);
    }
}

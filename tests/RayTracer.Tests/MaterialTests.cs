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
        Sphere s = new("s");
        Assert.Equal(new Material(), s.Material);
    }

    [Fact]
    public void SphereCanBeAssignedNewMaterial()
    {
        Sphere s = new("s");
        Material m = new();
        m.Ambient = 1;
        s.Material = m;
        Assert.Equal(m, s.Material);
    }
}

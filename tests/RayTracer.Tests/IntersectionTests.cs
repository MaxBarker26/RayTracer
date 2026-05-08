namespace RayTracer.Tests;

using RayTracer.Cli;

public class IntersectionTests
{
    [Fact]
    public void IntersectionConstructor_ANewIntersectionWithTheAppropriateDataIsConstructed()
    {
        Sphere s = new();
        Intersection i = new(3.5, s);

        Assert.Equal(3.5, i.T);
        Assert.Equal(s, i.Shape);
    }
}

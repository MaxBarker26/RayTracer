namespace RayTracer.Tests;

using RayTracer.Cli;

public class RayTests
{
    [Fact]
    public void RayConstructor_DirectionAndOriginProperties_InitializedCorrectly()
    {
        Ray r = new(new Point(1, 2, 3), new Vector(4, 5, 6));

        Assert.Equal(new Point(1, 2, 3), r.Origin);
        Assert.Equal(new Vector(4, 5, 6), r.Direction);
    }
}

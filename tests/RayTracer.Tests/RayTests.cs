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

    [Fact]
    public void Position_CalculateAPointBasedOnDistance_PointInExpectedPosition()
    {
        Ray r = new(new Point(2, 3, 4), new Vector(1, 0, 0));
        Assert.Equal(new Point(2, 3, 4), r.Position(0));
        Assert.Equal(new Point(3, 3, 4), r.Position(1));
        Assert.Equal(new Point(1, 3, 4), r.Position(-1));
        Assert.Equal(new Point(4.5, 3, 4), r.Position(2.5));
    }
}

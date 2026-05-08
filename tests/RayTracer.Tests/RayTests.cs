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

    [Fact]
    public void Intersects_RayIntersectingSphere_RayPassesThroughSphereAtExpectedPoints()
    {
        Sphere s = new();
        Ray r = new(new Point(0, 0, -5), new Vector(0, 0, 1));
        double[] xs = r.Intersects(s);
        Assert.Equal(2, xs.Length);
        Assert.Equal(4.0, xs[0]);
        Assert.Equal(6.0, xs[1]);
    }

    [Fact]
    public void Intersects_RayTangentToASphere_RayIntersectsAtOnePointOnTheSphere()
    {
        Sphere s = new();
        Ray r = new(new Point(0, 1, -5), new Vector(0, 0, 1));
        double[] xs = r.Intersects(s);
        Assert.Equal(5.0, xs[0]);
        Assert.Equal(5.0, xs[1]);
    }

    [Fact]
    public void Intersects_RayMissesSphere_RayIntersectsAtNoPointOnTheSphere()
    {
        Sphere s = new();
        Ray r = new(new Point(0, 2, -5), new Vector(0, 0, 1));
        double[] xs = r.Intersects(s);
        Assert.Equal(xs.Length, 0);
    }

    [Fact]
    public void Intersects_RayOriginatesInsideSphere_RayPassesThroughSphereAtExpectedPoints()
    {
        Sphere s = new();
        Ray r = new(new Point(0, 0, 0), new Vector(0, 0, 1));
        double[] xs = r.Intersects(s);
        Assert.Equal(xs.Length, 2);
        Assert.Equal(-1.0, xs[0]);
        Assert.Equal(1.0, xs[1]);
    }

    [Fact]
    public void Intersects_RayOriginatesBehindSphere_RayPassesThroughSphereAtExpectedPoints()
    {
        Sphere s = new();
        Ray r = new(new Point(0, 0, 5), new Vector(0, 0, 1));
        double[] xs = r.Intersects(s);
        Assert.Equal(xs.Length, 2);
        Assert.Equal(-6.0, xs[0]);
        Assert.Equal(-4.0, xs[1]);
    }
}

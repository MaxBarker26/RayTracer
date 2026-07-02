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
        Sphere s = new("s");
        Ray r = new(new Point(0, 0, -5), new Vector(0, 0, 1));
        var xs = r.Intersects(s);
        Assert.Equal(2, xs.Count);
        Assert.Equal(4.0, xs.Dequeue().T);
        Assert.Equal(6.0, xs.Dequeue().T);
    }

    [Fact]
    public void Intersects_RayTangentToASphere_RayIntersectsAtOnePointOnTheSphere()
    {
        Sphere s = new("s");
        Ray r = new(new Point(0, 1, -5), new Vector(0, 0, 1));
        var xs = r.Intersects(s);
        Assert.Equal(5.0, xs.Dequeue().T);
        Assert.Equal(5.0, xs.Dequeue().T);
    }

    [Fact]
    public void Intersects_RayMissesSphere_RayIntersectsAtNoPointOnTheSphere()
    {
        Sphere s = new("s");
        Ray r = new(new Point(0, 2, -5), new Vector(0, 0, 1));
        var xs = r.Intersects(s);
        Assert.Equal(xs.Count, 0);
    }

    [Fact]
    public void Intersects_RayOriginatesInsideSphere_RayPassesThroughSphereAtExpectedPoints()
    {
        Sphere s = new("s");
        Ray r = new(new Point(0, 0, 0), new Vector(0, 0, 1));
        var xs = r.Intersects(s);
        Assert.Equal(xs.Count, 2);
        Assert.Equal(-1.0, xs.Dequeue().T);
        Assert.Equal(1.0, xs.Dequeue().T);
    }

    [Fact]
    public void Intersects_RayOriginatesBehindSphere_RayPassesThroughSphereAtExpectedPoints()
    {
        Sphere s = new("s");
        Ray r = new(new Point(0, 0, 5), new Vector(0, 0, 1));
        var xs = r.Intersects(s);
        Assert.Equal(xs.Count, 2);
        Assert.Equal(-6.0, xs.Dequeue().T);
        Assert.Equal(-4.0, xs.Dequeue().T);
    }

    [Fact]
    public void Transform_TranslatingARay_ReturnNewTranslatedRay()
    {
        Ray r = new(new Point(1, 2, 3), new Vector(0, 1, 0));
        Matrix m = Matrix.Translation(3, 4, 5);
        Ray r2 = r.Transform(m);

        Assert.Equal(new Point(4, 6, 8), r2.Origin);
        Assert.Equal(new Vector(0, 1, 0), r2.Direction);
    }

    [Fact]
    public void Transform_ScalingARay_ReturnNewScaledRay()
    {
        Ray r = new(new Point(1, 2, 3), new Vector(0, 1, 0));
        Matrix m = Matrix.Scaling(2, 3, 4);
        Ray r2 = r.Transform(m);

        Assert.Equal(new Point(2, 6, 12), r2.Origin);
        Assert.Equal(new Vector(0, 3, 0), r2.Direction);
    }

    [Fact]
    public void Intersects_RayIntersectsAWorld_ReturnExpectedIntersections()
    {
        World w = World.Default();
        Ray r = new Ray(new(0, 0, -5), new(0, 0, 1));
        PriorityQueue<Intersection, double> xs = r.Intersects(w);

        Assert.Equal(4, xs.Count);
        Assert.Equal(4, xs.Dequeue().T);
        Assert.Equal(4.5, xs.Dequeue().T);
        Assert.Equal(5.5, xs.Dequeue().T);
        Assert.Equal(6, xs.Dequeue().T);
    }
}

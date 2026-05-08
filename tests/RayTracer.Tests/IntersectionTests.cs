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

    [Fact]
    public void ListOfIntersections_ListOfIntersectionsFunctionsCorrectly()
    {
        Sphere s = new();
        Intersection i1 = new(1, s);
        Intersection i2 = new(2, s);

        List<Intersection> xs = new(new Intersection[] { i1, i2 });

        Assert.Equal(2, xs.Count);
        Assert.Equal(1, xs[0].T);
        Assert.Equal(2, xs[1].T);
    }

    [Fact]
    public void Hit_HitFunctionWithPositiveTIntersections_ReturnsCorrectIntersection()
    {
        Sphere s = new();
        Intersection i1 = new(1, s);
        Intersection i2 = new(1, s);
        PriorityQueue<Intersection, double> xs = new();
        xs.Enqueue(i1, i1.T);
        xs.Enqueue(i2, i2.T);
        Intersection? i = Intersection.Hit(xs);
        Assert.Equal(i1, i);
    }

    [Fact]
    public void Hit_HitFunctionWithSomeNegativeTIntersections_ReturnsCorrectIntersection()
    {
        Sphere s = new();
        Intersection i1 = new(-1, s);
        Intersection i2 = new(1, s);
        PriorityQueue<Intersection, double> xs = new();
        xs.Enqueue(i1, i1.T);
        xs.Enqueue(i2, i2.T);
        Intersection? i = Intersection.Hit(xs);
        Assert.Equal(i2, i);
    }

    [Fact]
    public void Hit_HitFunctionWithAllNegativeTIntersections_ReturnsCorrectIntersection()
    {
        Sphere s = new();
        Intersection i1 = new(-1, s);
        Intersection i2 = new(-1, s);
        PriorityQueue<Intersection, double> xs = new();
        xs.Enqueue(i1, i1.T);
        xs.Enqueue(i2, i2.T);
        Intersection? i = Intersection.Hit(xs);
        Assert.Equal(null, i);
    }

    [Fact]
    public void Hit_HitFunctionReturnsLowestNonnegativeIntersection_ReturnsCorrectIntersection()
    {
        Sphere s = new();
        Intersection i1 = new(5, s);
        Intersection i2 = new(7, s);
        Intersection i3 = new(-3, s);
        Intersection i4 = new(2, s);
        PriorityQueue<Intersection, double> xs = new();
        xs.Enqueue(i1, i1.T);
        xs.Enqueue(i2, i2.T);
        xs.Enqueue(i3, i3.T);
        xs.Enqueue(i4, i4.T);
        Intersection? i = Intersection.Hit(xs);
        Assert.Equal(i4, i);
    }
}

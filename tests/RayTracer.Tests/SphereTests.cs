namespace RayTracer.Tests;

using RayTracer.Cli;

public class SphereTests
{
    [Fact]
    public void TransformMatrixProperty_DefaultPropertyValue_EqualsIdentityMatrix()
    {
        Sphere s = new();
        Assert.Equal(Matrix.Identity(4), s.TransformMatrix);
    }

    [Fact]
    public void TransformMatrixProperty_ChangingPropertyValue_EqualsNewTransformationMatrix()
    {
        Sphere s = new();
        Matrix t = Matrix.Translation(2, 3, 4);
        s.TransformMatrix = t;
        Assert.Equal(t, s.TransformMatrix);
    }

    [Fact]
    public void RayIntersectingSphere_IntersectsScaledSphereAtExpectedPoints()
    {
        Ray r = new(new Point(0, 0, -5), new Vector(0, 0, 1));
        Sphere s = new();
        s.TransformMatrix = Matrix.Scaling(2, 2, 2);

        double[] xs = r.Intersects(s);
        Assert.Equal(3, xs[0]);
        Assert.Equal(7, xs[1]);
    }

    [Fact]
    public void RayIntersectingSphere_IntersectsTranslatedSphereAtExpectedPoints()
    {
        Ray r = new(new Point(0, 0, -5), new Vector(0, 0, 1));
        Sphere s = new();
        s.TransformMatrix = Matrix.Translation(2, 2, 2);

        double[] xs = r.Intersects(s);
        Assert.Equal(0, xs.Length);
    }
}

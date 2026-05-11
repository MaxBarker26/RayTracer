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

        var xs = r.Intersects(s);
        Assert.Equal(3, xs.Dequeue().T);
        Assert.Equal(7, xs.Dequeue().T);
    }

    [Fact]
    public void RayIntersectingSphere_IntersectsTranslatedSphereAtExpectedPoints()
    {
        Ray r = new(new Point(0, 0, -5), new Vector(0, 0, 1));
        Sphere s = new();
        s.TransformMatrix = Matrix.Translation(2, 2, 2);

        var xs = r.Intersects(s);
        Assert.Equal(0, xs.Count);
    }

    [Fact]
    public void NormalAt_FindNormalVectorOnASphereOnXAxis_ReturnsVector()
    {
        Sphere s = new();
        Vector n = s.NormalAt(new(1, 0, 0));
        Assert.Equal(new Vector(1, 0, 0), n);
    }

    [Fact]
    public void NormalAt_FindNormalVectorOnASphereOnYAxis_ReturnsVector()
    {
        Sphere s = new();
        Vector n = s.NormalAt(new(0, 1, 0));
        Assert.Equal(new Vector(0, 1, 0), n);
    }

    [Fact]
    public void NormalAt_FindNormalVectorOnASphereOnZAxis_ReturnsVector()
    {
        Sphere s = new();
        Vector n = s.NormalAt(new(0, 0, 1));
        Assert.Equal(new Vector(0, 0, 1), n);
    }

    [Fact]
    public void NormalAt_FindNormalVectorOnASphereNonAxialPoint_ReturnsVector()
    {
        Sphere s = new();
        Vector n = s.NormalAt(new(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));
        Assert.Equal(new Vector(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3), n);
    }

    [Fact]
    public void NormalAt_NormalIsNormalizedVector_ReturnsNormalizedVector()
    {
        Sphere s = new();
        Vector n = s.NormalAt(new(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));
        Assert.Equal(n.Normalized, n);
    }

    [Fact]
    public void NormalAt_NormalOnTranslatedSphere_ReturnsVector()
    {
        Sphere s = new();
        s.TransformMatrix = Matrix.Translation(0, 1, 0);
        Vector n = s.NormalAt(new(0, 1.70711, -0.70711));
        Assert.Equal(new Vector(0, 0.70711, -0.70711), n);
    }

    [Fact]
    public void NormalAt_NormalOnMultiTransformedSphere_ReturnsVector()
    {
        Sphere s = new();
        s.TransformMatrix = Matrix.Scaling(1, 0.5, 1) * Matrix.RotationZ(Math.PI / 5);
        Vector n = s.NormalAt(new(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2));
        Assert.Equal(new Vector(0, 0.97014, -0.24254), n);
    }
}

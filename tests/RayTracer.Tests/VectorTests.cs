namespace RayTracer.Tests;

using RayTracer.Cli;

public class VectorTests()
{
    ///<summary>
    /// Tests the magnitude property which is unique to the Vector class.
    ///</summary>
    [Fact]
    public void Magnitude_TheMagnitudeOfAVectorObject_MagnatudeEqualsExpected()
    {
        Vector vector = new(2, 3, 4);
        double expectedMagnitude = Math.Sqrt(29);
        Assert.Equal(expectedMagnitude, vector.Magnitude, 0.000000001);
    }

    ///<summary>
    /// Tests the Normalized property which is unique to the Vector class.
    ///</summary>
    [Fact]
    public void Normalized_ANormalizedVersionOfAGivenVector_NormalizedVectorEqualsExpected()
    {
        Vector vector = new(2, 3, 4);
        double magnitude = Math.Sqrt(29);
        Vector expectedNormalized = new(2 / magnitude, 3 / magnitude, 4 / magnitude);
        Assert.Equal(expectedNormalized, vector.Normalized);
    }

    [Fact]
    public void Dot_DotProductOfTwoVectorsProducesAScalar_ResultingScalarIsAsExpected()
    {
        Vector v1 = new(3, 6, 9);
        Vector v2 = new(2, 3, 4);
        double expectedScalar = 60;
        Assert.Equal(expectedScalar, v1.Dot(v2));
    }

    [Fact]
    public void Cross_CrossProductOfTwoVectorsProducesAVector_ResultingVectorIsAsExpected()
    {
        Vector v1 = new(1, 2, 3);
        Vector v2 = new(2, 3, 4);
        Assert.Equal(new Vector(-1, 2, -1), v1.Cross(v2));
        Assert.Equal(new Vector(1, -2, 1), v2.Cross(v1));
    }

    [Fact]
    public void Operators_OperatorsWorkOnVectorClass()
    {
        Vector v1 = new(1, 2, 3);
        Vector v2 = new(2, 3, 4);
        Assert.Equal(new Vector(-1, -1, -1), v1 - v2);
        Assert.Equal(new Vector(3, 5, 7), v1 + v2);
        Assert.Equal(new Vector(3, 6, 9), v1 * 3);
    }

    [Fact]
    public void Operators_VectorSpecificOperatorOverloads_DoNotThrow()
    {
        Vector v1 = new(1, 2, 3);
        Vector v2 = new(2, 3, 4);

        Vector v3 = v1 - v2;
        Vector v4 = v1 + v2;
        Vector v5 = v1 * 2;
        Vector v6 = v2 / 2;
    }

    [Fact]
    public void Reflect_ReflectingVectorApproachingAt45Degrees_ReturnsReflectedVector()
    {
        Vector v = new(1, -1, 0);
        Vector n = new(0, 1, 0);
        Vector r = v.Reflect(n);
        Assert.Equal(new Vector(1, 1, 0), r);
    }

    [Fact]
    public void Reflect_ReflectingVectorOffSlantedSurface_ReturnsReflectedVector()
    {
        Vector v = new(0, -1, 0);
        Vector n = new(Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0);
        Vector r = v.Reflect(n);
        Assert.Equal(new Vector(1, 0, 0), r);
    }
}

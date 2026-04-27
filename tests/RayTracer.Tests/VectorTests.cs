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
}

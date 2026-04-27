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
}

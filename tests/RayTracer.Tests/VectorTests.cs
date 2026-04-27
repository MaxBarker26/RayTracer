namespace RayTracer.Tests;

using RayTracer.Cli;

public class VectorTests()
{
    [Fact]
    public void Magnitude_ReturnTheMagnitudeOfAVectorObject_MagnatudeEqualsExpected()
    {
        Vector vector = new(2, 3, 4);
        double expectedMagnitude = Math.Sqrt(29);
        Assert.Equal(expectedMagnitude, vector.Magnitude, 0.000000001);
    }
}

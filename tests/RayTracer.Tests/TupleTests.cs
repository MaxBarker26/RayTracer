namespace RayTracer.Tests;

using RayTracer.Cli;

public class TupleTests
{
    ///<summary>
    /// A tuple with a W value of 1.0 is a point.
    ///</summary>
    [Fact]
    public void IsPoint_DeterminesIfTupleIsAPoint_True()
    {
        Tuple point = new Tuple(5.2, -6.3, 1.7, 1.0);
        Assert.Equal(point.W, 1.0);
        Assert.True(point.IsPoint());
    }

    ///<summary>
    /// A tuple with a W value of 0 is a vector.
    ///</summary>
    [Fact]
    public void IsVector_DeterminesIfTupleIsAVector_True()
    {
        Tuple vector = new Tuple(5.2, -6.3, 1.7, 0);
        Assert.Equal(vector.W, 0);
        Assert.True(vector.IsVector());
    }
}

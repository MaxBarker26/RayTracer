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

    ///<summary>
    /// Creating a point with the Point() static method is equivalent
    /// to creating a tuple with the same xyz coordinates and a w value
    /// equal to 1.0.
    ///</summary>
    [Fact]
    public void Point_CreatesAPoint_NewPointIsEquivalentToTuple()
    {
        Tuple point = Tuple.Point(6.1, 6.2, -81.6);
        Tuple comparisonPoint = new Tuple(6.1, 6.2, -81.6, 1.0);
        Assert.Equal(comparisonPoint, point);
    }

    ///<summary>
    /// Creating a point with the Vector() static method is equivalent
    /// to creating a tuple with the same xyz coordinates and a w value
    /// equal to 0.
    ///</summary>
    [Fact]
    public void Vector_CreatesAVector_NewVectorIsEquivalentToTuple()
    {
        Tuple vector = Tuple.Vector(6.1, 6.2, -81.6);
        Tuple comparisonVector = new Tuple(6.1, 6.2, -81.6, 0);
        Console.WriteLine(vector.Equals(comparisonVector));
        Assert.Equal(comparisonVector, vector);
    }
}

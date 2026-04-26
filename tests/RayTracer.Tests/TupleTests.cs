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
    public void Point_CreatesAPoint_NewPointIsEquivalentToExpectedTuple()
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
    public void Vector_CreatesAVector_NewVectorIsEquivalentToExpectedTuple()
    {
        Tuple vector = Tuple.Vector(6.1, 6.2, -81.6);
        Tuple comparisonVector = new Tuple(6.1, 6.2, -81.6, 0);
        Console.WriteLine(vector.Equals(comparisonVector));
        Assert.Equal(comparisonVector, vector);
    }

    ///<summary>
    /// Adding a point and vector together should result in a point which represents
    /// the point arrived at after traveling along the vector from the given point.
    ///</summary>
    [Fact]
    public void Add_CombineVectorAndPointViaAddition_ReturnsExpectedPoint()
    {
        Tuple point = Tuple.Point(3, -5, 1);
        Tuple vector = Tuple.Vector(1, -4, -6);
        Tuple destinationPoint = point + vector;
        Tuple comparisonPoint = new Tuple(4, -9, -5, 1);
        Assert.Equal(comparisonPoint, destinationPoint);
    }

    ///<summary>
    /// Adding two vectors together should result in a vector which represents
    /// sum magnitude and direction of the two vectors.
    ///</summary>
    [Fact]
    public void Add_CombineTwoVectorsViaAddition_ReturnsExpectedVector()
    {
        Tuple vectorA = Tuple.Vector(3, -5, 1);
        Tuple vectorB = Tuple.Vector(1, -4, -6);
        Tuple resultingVector = vectorA + vectorB;
        Tuple comparisonVector = new Tuple(4, -9, -5, 0);
        Assert.Equal(comparisonVector, resultingVector);
    }

    ///<summary>
    /// Adding two points together should result in a w value of 2, neither a point or vector.
    /// This will cause an exception to be thrown.
    ///</summary>
    [Fact]
    public void Add_CombineTwoPointsViaAddition_ThrowsInvalidOperationException()
    {
        Tuple pointA = Tuple.Point(3, -5, 1);
        Tuple pointB = Tuple.Point(1, -4, -6);
        Assert.Throws<InvalidOperationException>(() => pointA + pointB);
    }

    ///<summary>
    /// Subtracting two points will not throw an exception it will instead result in
    /// a vector.
    ///</summary>
    [Fact]
    public void Subtract_CombineTwoPointsViaSubtraction_ReturnsExpectedVector()
    {
        Tuple pointA = Tuple.Point(3, -5, 1);
        Tuple pointB = Tuple.Point(1, -4, -6);
        Tuple resultingVector = pointA - pointB;
        Tuple comparisonVector = new Tuple(2, -1, 7, 0);
        Assert.Equal(comparisonVector, resultingVector);
    }

    ///<summary>
    /// Subtracting a vector from a point is valid and will result in a point.
    ///</summary>
    [Fact]
    public void Subtract_CombineVectorAndPointViaSubtraction_ReturnsExpectedPoint()
    {
        Tuple point = Tuple.Point(3, -5, 1);
        Tuple vector = Tuple.Vector(1, -4, -6);
        Tuple resultingVector = point - vector;
        Tuple comparisonVector = new Tuple(2, -1, 7, 1);
        Assert.Equal(comparisonVector, resultingVector);
    }

    ///<summary>
    /// Subtracting a vector from a vector is valid and will result in a vector.
    ///</summary>
    [Fact]
    public void Subtract_CombineTwoVectorsViaSubtraction_ReturnsExpectedVector()
    {
        Tuple vectorA = Tuple.Vector(3, -5, 1);
        Tuple vectorB = Tuple.Vector(1, -4, -6);
        Tuple resultingVector = vectorA - vectorB;
        Tuple comparisonVector = new Tuple(2, -1, 7, 0);
        Assert.Equal(comparisonVector, resultingVector);
    }

    ///<summary>
    /// Subtracting a point from a vector will result in a negative w property
    /// for the resulting tuple and therefore is not valid.
    ///</summary>
    [Fact]
    public void Subtract_SubtractPointFromVector_ThrowsInvalidOperationException()
    {
        Tuple vector = Tuple.Vector(3, -5, 1);
        Tuple point = Tuple.Point(1, -4, -6);
        Assert.Throws<InvalidOperationException>(() => vector - point);
    }

    ///<summary>
    /// Eaach individual double in a tuple will be swapped to it's inverse as a result
    /// of the unary minus operation.
    ///</summary>
    [Fact]
    public void NegativeUnaryOperator_NegateATuple_TuplePropertiesAreNegated()
    {
        Tuple vector = Tuple.Vector(2.3, -6.7, 9.846);
        Tuple negated = -vector;
        Tuple expected = new Tuple(-2.3, 6.7, -9.846, 0);
        Assert.Equal(expected, negated);
    }

    ///<summary
    /// Multiplying a tuple by a scalar greater that one
    /// lengthening of the each vector property by the scalar.
    ///</summary>
    [Fact]
    public void ScalarMultiply_MultiplyATupleByAScalarValue_TuplePropertiesScaleAsExpected()
    {
        Tuple vector = Tuple.Vector(3, -7, -11);
        double scalar = 3;
        Tuple scaledVector = vector * scalar;
        Tuple expected = new Tuple(9, -21, -33, 0);
    }

    ///<summary
    /// Multiplying a tuple by a scalar less than one will result in a
    /// shortening of the each vector property by the scalar fraction.
    ///</summary>
    [Fact]
    public void ScalarMultiply_MultiplyATupleByAScalarValueLessThan1_TuplePropertiesScaleAsExpected()
    {
        Tuple vector = Tuple.Vector(3, -7, -11);
        double scalar = 0.5;
        Tuple scaledVector = vector * scalar;
        Tuple expected = new Tuple(1.5, -3.5, -5.5, 0);
    }
}

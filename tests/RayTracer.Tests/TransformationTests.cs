namespace RayTracer.Tests;

using RayTracer.Cli;

public class TransformationTests
{
    [Fact]
    public void Translation_MultiplyingTranslationMatrixAndPoint_ReturnsExpectedPoint()
    {
        Matrix transform = Matrix.Translation(5, -3, 2);

        Point p = new Point(-3, 4, 5);

        Assert.Equal(new Point(2, 1, 7), transform * p);
    }

    [Fact]
    public void Translation_MultiplyingPointAndInverseTranslationMatrix_PointMovesInReverse()
    {
        Matrix inverse = Matrix.Translation(5, -3, 2).Invert();
        Point p = new Point(-3, 4, 5);
        Assert.Equal(new Point(-8, 7, 3), inverse * p);
    }

    [Fact]
    public void Translation_MultiplyingTranslationMatrixAndVector_ResultingVectorIsUnchanged()
    {
        Matrix transform = Matrix.Translation(5, -3, 2);

        Vector v = new Vector(-3, 4, 5);

        Assert.Equal(v, transform * v);
    }

    [Fact]
    public void Scaling_ScalingMatrixTimesAPoint_ResultingPointIsCorrect()
    {
        Matrix transform = Matrix.Scaling(2, 3, 4);
        Point p = new(-4, 6, 8);
        Assert.Equal(new Point(-8, 18, 32), transform * p);
    }

    [Fact]
    public void Scaling_ScalingMatrixTimesVector_ResultingVectorIsScaledAppropriately()
    {
        Matrix transform = Matrix.Scaling(2, 3, 4);
        Vector v = new(-4, 6, 8);
        Assert.Equal(new Vector(-8, 18, 32), transform * v);
    }

    [Fact]
    public void Scaling_InverseScalingMatrixTimesVector_ResultingVectorIsShrunkAppropriately()
    {
        Matrix inverse = Matrix.Scaling(2, 3, 4).Invert();
        Vector v = new(-4, 6, 8);
        Assert.Equal(new Vector(-2, 2, 2), inverse * v);
    }

    [Fact]
    public void RotationX_RotatingAPointAroundXAxis_PointRotatesAsExpected()
    {
        Point p = new(0, 1, 0);
        Matrix halfQuarter = Matrix.RotationX(Math.PI / 4);
        Matrix fullQuarter = Matrix.RotationX(Math.PI / 2);
        Assert.Equal(new Point(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2), halfQuarter * p);
        Assert.Equal(new Point(0, 0, 1), fullQuarter * p);
    }

    [Fact]
    public void RotationX_RotatingAPointAroundXAxisInverseRotation_PointRotatesOpposite()
    {
        Point p = new(0, 1, 0);
        Matrix halfQuarter = Matrix.RotationX(Math.PI / 4).Invert();
        Assert.Equal(new Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2), halfQuarter * p);
    }

    [Fact]
    public void RotationY_RotatingAPointAroundYAxis_PointRotatesCorrectly()
    {
        Point p = new(0, 0, 1);

        Matrix halfQuarter = Matrix.RotationY(Math.PI / 4);
        Matrix fullQuarter = Matrix.RotationY(Math.PI / 2);
        Assert.Equal(new Point(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2), halfQuarter * p);
        Assert.Equal(new Point(1, 0, 0), fullQuarter * p);
    }

    [Fact]
    public void RotationZ_RotatingAPointAroundZAxis_PointRotatesCorrectly()
    {
        Point p = new(0, 1, 0);

        Matrix halfQuarter = Matrix.RotationZ(Math.PI / 4);
        Matrix fullQuarter = Matrix.RotationZ(Math.PI / 2);
        Assert.Equal(new Point(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0), halfQuarter * p);
        Assert.Equal(new Point(-1, 0, 0), fullQuarter * p);
    }

    [Fact]
    public void Shearing_TransformXInProportionToY_PointIsExpected()
    {
        Point p = new(2, 3, 4);
        Matrix transform = Matrix.Shearing(1, 0, 0, 0, 0, 0);

        Assert.Equal(new Point(5, 3, 4), transform * p);
    }

    [Fact]
    public void Shearing_TransformXInProportionToZ_PointIsExpected()
    {
        Point p = new(2, 3, 4);
        Matrix transform = Matrix.Shearing(0, 1, 0, 0, 0, 0);

        Assert.Equal(new Point(6, 3, 4), transform * p);
    }

    [Fact]
    public void Shearing_TransformYInProportionToX_PointIsExpected()
    {
        Point p = new(2, 3, 4);
        Matrix transform = Matrix.Shearing(0, 0, 1, 0, 0, 0);

        Assert.Equal(new Point(2, 5, 4), transform * p);
    }

    [Fact]
    public void Shearing_TransformYInProportionToZ_PointIsExpected()
    {
        Point p = new(2, 3, 4);
        Matrix transform = Matrix.Shearing(0, 0, 0, 1, 0, 0);

        Assert.Equal(new Point(2, 7, 4), transform * p);
    }

    [Fact]
    public void Shearing_TransformZInProportionToX_PointIsExpected()
    {
        Point p = new(2, 3, 4);
        Matrix transform = Matrix.Shearing(0, 0, 0, 0, 1, 0);

        Assert.Equal(new Point(2, 3, 6), transform * p);
    }

    [Fact]
    public void Shearing_TransformZInProportionToY_PointIsExpected()
    {
        Point p = new(2, 3, 4);
        Matrix transform = Matrix.Shearing(0, 0, 0, 0, 0, 1);

        Assert.Equal(new Point(2, 3, 7), transform * p);
    }

    [Fact]
    public void TransformationInstanceMethodsForPoints_InstanceMethodsReturnSameAsMulitiplication()
    {
        Tuple p = new(2, 3, 4, 1);
        Tuple p2 = new(0, 1, 0, 1);

        Assert.Equal(new Point(2, 3, 7), p.Shear(0, 0, 0, 0, 0, 1));
        Assert.Equal(new Point(0, 0, 1), p2.RotateX(Math.PI / 2));
    }

    [Fact]
    public void ChainingTupleTransformations_TransformationsChainCorrectly()
    {
        Tuple p = new(1, 0, 1, 1);
        Tuple t = p.RotateX(Math.PI / 2).Scale(5, 5, 5).Translate(10, 5, 7);

        Assert.Equal(new Point(15, 0, 7), t);
    }
}

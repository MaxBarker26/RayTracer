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
}

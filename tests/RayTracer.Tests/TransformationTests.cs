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
}

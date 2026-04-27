namespace RayTracer.Tests;

using RayTracer.Cli;

public class ColorTests()
{
    [Fact]
    public void Add_AddingTwoColorsProducesNewColor_RGBValuesAreExpected()
    {
        Color c1 = new(0.2, 0.7, 0.9);
        Color c2 = new(0.2, 0.7, 0.1);

        Color sumColor = c1 + c2;
        Assert.Equal(new Color(0.4, 1.4, 1.0), sumColor);
    }

    [Fact]
    public void Subtract_SubtractingTwoColorsProducesNewColor_RGBValuesAreExpected()
    {
        Color c1 = new(0.2, 0.7, 0.9);
        Color c2 = new(0.2, 0.7, 0.1);

        Color sumColor = c1 - c2;
        Assert.Equal(new Color(0, 0, 0.8), sumColor);
    }

    [Fact]
    public void ScalarMultiply_MultiplyAColorByAScalarProducesNewColor_RGBValuesAreExpected()
    {
        Color c1 = new(0.2, 0.7, 0.9);

        Color sumColor = c1 * 2;
        Assert.Equal(new Color(0.4, 1.4, 1.8), sumColor);
    }

    [Fact]
    public void ScalarDivide_DividingAColorByAScalarProducesNewColor_RGBValuesAreExpected()
    {
        Color c1 = new(0.2, 0.7, 0.9);

        Color sumColor = c1 / 2;
        Assert.Equal(new Color(0.1, 0.35, 0.45), sumColor);
    }

    [Fact]
    public void HadamardProduct_TheHadamardProductOfTwoColorsProducesANewColor_RGBValuesAreExpected()
    {
        Color c1 = new(0.2, 0.7, 0.9);
        Color c2 = new(0.2, 0.7, 0.1);

        Color sumColor = c1.Prod(c2);
        Assert.Equal(new Color(0.04, 0.49, 0.09), sumColor);
    }
}

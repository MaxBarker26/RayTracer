namespace RayTracer.Tests;

using RayTracer.Cli;

public class WorldTests()
{
    [Fact]
    public void WorldConstructor_NewWorldIsEmpty()
    {
        World w = new();
        Assert.Equal(0, w.Objects.Count);
        Assert.Null(w.LightSource);
    }

    [Fact]
    public void DefaultWorld_ContainsDefaultLightSourceAndObjects()
    {
        World w = World.Default();
        //change this test to test the properties of the default world's objects / light
        Assert.Equal(new Color(0.8, 1.0, 0.6), w.Objects[0].Material.Color);
        Assert.Equal(0.7, w.Objects[0].Material.Diffuse);
        Assert.Equal(0.2, w.Objects[0].Material.Specular);

        Assert.Equal(Matrix.Scaling(0.5, 0.5, 0.5), w.Objects[1].TransformMatrix);

        PointLight compareLight = new(new(-10, 10, -10), new(1, 1, 1));
        Assert.Equal(compareLight.Intensity, w.LightSource?.Intensity);
        Assert.Equal(compareLight.Position, w.LightSource?.Position);
    }
}
